using NAudio.Dsp;
using NAudio.Wave;
using System.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;

using Complex = NAudio.Dsp.Complex;

namespace AudioProcessing.Core
{
    public class AudioToData
    {
        public sealed class AudioAnalysisData
        {
            public int fftSize { get; }
            public int SampleRate { get; }
            public int HopSize { get; }
            public int BandCount { get; }
            public int FrameCount { get; } 

            public ReadOnlyMemory<float> Frames { get; private set; }

            public AudioAnalysisData(int fftSize, int sampleRate, int hopSize, int bandCount, float[] frames)
            {
                this.fftSize = fftSize;
                this.SampleRate = sampleRate;
                this.HopSize = hopSize;
                this.BandCount = bandCount;
                this.Frames = new ReadOnlyMemory<float>(frames);
                this.FrameCount = frames.Length / bandCount;
            }
          
            public float this[int frame, int band]
            {

                get
                {
                    if ((uint)frame >= (uint)FrameCount || (uint)band >= (uint)BandCount)
                        throw new ArgumentOutOfRangeException();

                    return Frames.Span[frame * BandCount + band];
                }
            }


            public ReadOnlySpan<float> GetFrameData(int currentFrame)
            {
                if (currentFrame < 0 || currentFrame >= FrameCount)
                {
                    return ReadOnlySpan<float>.Empty;
                }             
                int startIdx = currentFrame * BandCount;
                return this.Frames.Span.Slice(startIdx, BandCount);
            }

            public void ReleaseFrames()
            {
                this.Frames = ReadOnlyMemory<float>.Empty;
            }

            /// <summary>
            /// Normalize the frame data to a 0-1 range based on the provided silence threshold.
            /// Values below the silence threshold will be treated as 0, and values above will be scaled accordingly. 
            /// The normalized data will be stored in the provided normalizedData span, which should have a length of at least FrameCount * BandCount.
            /// </summary>
            /// <param name="normalizedData"></param>
            /// <param name="silenceThreshold"> Maxium = 0 </param>
            public void NormalizeFrameData(Span<float> normalizedData,int silenceThreshold)
            {
                if(normalizedData.Length != FrameCount * BandCount)
                {
                    throw new ArgumentException($"Normalized data length != {FrameCount*BandCount}");
                }
                for (int i = 0; i < FrameCount; i++)
                {
                    int idx = i * BandCount;
                    ReadOnlySpan<float> frameData = this.Frames.Span.Slice(idx, BandCount);

                    for (int j = 0; j < BandCount; j++)
                    {
                        float val = MathF.Max(frameData[j], silenceThreshold);
                        normalizedData[idx + j] = (val - silenceThreshold) / (-silenceThreshold);
                    }
                }
               
            }

        }


        private readonly int _fftSize;
        public int BandCount { get; }
        private readonly float[] _hanningWindow;
        public AudioToData(int fftWindowSize,int bandCount)
        {
            if (!((fftWindowSize & (fftWindowSize - 1)) == 0 && fftWindowSize > 0))
            {
                throw new ArgumentException("FFT size must be power of two", nameof(fftWindowSize));
            }
            if (bandCount <= 0)
            {
                throw new ArgumentException("Band count must be positive", nameof(bandCount));
            }
            int halfSize = fftWindowSize / 2;

            int minBinsPerBand = 4;
            int maxBandCount = halfSize / minBinsPerBand;
            if (bandCount > maxBandCount)
            {
                throw new ArgumentException("Band count is too large for the given FFT size, max allowed is " + maxBandCount, nameof(bandCount));
            }

            _fftSize = fftWindowSize;
            BandCount = bandCount;

            _hanningWindow = new float[_fftSize];
            CreateHanningWindow(_hanningWindow);
        }

        private void CreateHanningWindow(float[] windows)
        {

            for (int i = 0; i < this._fftSize; i++)
            {
                windows[i] = 0.5f - 0.5f * MathF.Cos(MathF.PI * 2.0f * i /(this._fftSize - 1));
            }
        }


        private AudioAnalysisData? LoadAudio(string filePath)
        {
            

            if (!(filePath is not null && File.Exists(filePath)))
            {
                //throw new FileNotFoundException();
                return null;
            }

            var listSample = new List<float>();
            int sampleRate;
            using (var reader = new AudioFileReader(filePath))
            {
                

                listSample.Capacity = (int)reader.Length / sizeof(float);
                sampleRate = reader.WaveFormat.SampleRate;
                int chanels = reader.WaveFormat.Channels;
                //analysisData.SampleRate = reader.WaveFormat.SampleRate;
                var buffers = new float[sampleRate * chanels];
                int readLength;

                while ((readLength = reader.Read(buffers, 0, buffers.Length)) > 0)
                {
                    //read only one channel,
                    //this can be adjusted to read multiple channels.
                    //single channel is enough for visualization
                    for (int i = 0; i < readLength; i += chanels)
                    {
                        listSample.Add(buffers[i]);
                    }
                }
            }

            if (listSample.Count < this._fftSize)
            {
                return null;
            }

            return GetData(CollectionsMarshal.AsSpan(listSample), sampleRate);
        }


        private AudioAnalysisData? GetData(ReadOnlySpan<float> samples, int sampleRate)
        {
            // hop size is half of fft size, this can be adjusted
            int hopSize = this._fftSize / 2;


            int m = BitOperations.Log2((uint)this._fftSize);

            int frameCount = (samples.Length - this._fftSize) / hopSize + 1;

            // half size for real signal, this is different concept to hop size even they have same number, do not adjust this. 
            int halfSize = this._fftSize / 2;

            // number of frequency bands, this can be adjusted, but should be divisor of halfSize. This represents each frame's band number.

            int bandCount = BandCount;

            // bins per band for linear average;
            //int binsperBand = halfSize / bandCount;


            var fftBuffer = ArrayPool<Complex>.Shared.Rent(this._fftSize);
            var magnitudes = ArrayPool<float>.Shared.Rent(halfSize);


            float[] bandFrames = new float[frameCount * bandCount];

            float minHz = 20f;       
            float maxHz = sampleRate / 2f;

          
            float melMin = 2595f * MathF.Log10(1f + minHz / 700f);
            float melMax = 2595f * MathF.Log10(1f + maxHz / 700f);

            int[] bandBoundaries = new int[bandCount + 1];
           
            bandBoundaries[0] = 0;
            bandBoundaries[bandCount] = halfSize;

            // mel scale for log average;
            for (int b = 1; b < bandCount; b++)
            {  
               
                float fraction = (float)b / bandCount;
                float currentMel = melMin + fraction * (melMax - melMin);

               
                float currentHz = 700f * (MathF.Pow(10f, currentMel / 2595f) - 1f);

                
                int bin = (int)MathF.Round(currentHz * this._fftSize / sampleRate);

              
                if (bin <= bandBoundaries[b - 1])
                {
                    bin = bandBoundaries[b - 1] + 1;
                }

               
                bandBoundaries[b] = Math.Min(bin, halfSize);
            }

           
            

            try
            {
                
                for (int frame = 0, offset = 0; frame < frameCount; frame++, offset += hopSize)
                {

                    for (int j = 0; j < this._fftSize; j++)
                    {
                        float s = samples[offset + j];
                        fftBuffer[j].X = s * _hanningWindow[j];
                        fftBuffer[j].Y = 0;
                    }

                    FastFourierTransform.FFT(true, m, fftBuffer);


                    for (int j = 0; j < halfSize; j++)
                    {
                        
                        magnitudes[j] = 4f * (fftBuffer[j].X * fftBuffer[j].X + fftBuffer[j].Y * fftBuffer[j].Y); 
                    }


                    for (int b = 0; b < bandCount; b++)
                    {
                        int startBin = bandBoundaries[b];
                        int endBin = bandBoundaries[b + 1];
                        
                        
                        float sum = 0;
                        float peak = 0;

                        

                        for (int k = startBin; k <endBin; k++)
                        {
                            float pwr = magnitudes[k];
                            sum += pwr;
                            //if (pwr > peak) peak = pwr;
                            peak = MathF.Max(peak, pwr);
                        }

                        int currentBinsInBand = Math.Max(1, endBin - startBin);
                        float mean = sum / currentBinsInBand;



                


                        float blendedMagnitude = (peak * 0.7f) + (mean * 0.3f);
                        
                        float finalDb = 10 * MathF.Log10(blendedMagnitude);

                        
                       

                        bandFrames[frame * bandCount + b] = finalDb;
                    }


                }
                return new AudioAnalysisData(this._fftSize, sampleRate, hopSize, bandCount, bandFrames);
            }
            finally
            {
                ArrayPool<Complex>.Shared.Return(fftBuffer, clearArray: false);
                ArrayPool<float>.Shared.Return(magnitudes, clearArray: false);
            }
        }


      
        public AudioAnalysisData Process(string filePath)
        {
            try
            {
                return LoadAudio(filePath)!;
            }
            catch
            {
                return null!;
            }
        }


    }

}