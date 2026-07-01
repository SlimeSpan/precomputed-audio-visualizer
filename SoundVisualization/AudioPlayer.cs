using System;
using System.Collections.Generic;
using System.Text;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioPlay
{ 
    internal class AudioPlayer
    {
        private WaveOutEvent _wavePlayer;
        private MixingSampleProvider _mixingSampleProvider;
        private AudioFileReader? _currentAudioFile;
        private ISampleProvider? _currentAudio;

        public sealed class AudioPlayerLatency
        {
            public const int LowLatency = 50;
            public const int DefaultLatency = 100;
            public const int HighLatency = 200;
        }
        public float Volume 
        {
            get => Volume;
            set 
            {
                _currentAudioFile?.Volume = value;
            } 
        }

        public AudioPlayer(int latency)
        {
            _wavePlayer = new WaveOutEvent() { DesiredLatency = latency };
            _mixingSampleProvider = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2));
            _mixingSampleProvider.ReadFully = true;

            _wavePlayer.Init(_mixingSampleProvider);
            
            _wavePlayer.Play();
        }


        public void PlayAudio(string filePath)
        {

            StopAudioInternal();
            _currentAudioFile = new AudioFileReader(filePath);

            var resampledSorce = new WdlResamplingSampleProvider(_currentAudioFile, 44100);
            if (resampledSorce.WaveFormat.Channels == 1)
            {
                _currentAudio = new MonoToStereoSampleProvider(resampledSorce);
            }
            else
            {
                _currentAudio = resampledSorce;
            }

            _mixingSampleProvider.AddMixerInput(_currentAudio);

            if(_wavePlayer.PlaybackState != PlaybackState.Playing)
            {
                _wavePlayer.Play();
            }
        }

        public void PauseAudio()
        {
            if (_wavePlayer is null)
            {
                return;
            }

            if (_wavePlayer.PlaybackState == PlaybackState.Playing)
            {
                _wavePlayer.Pause();
            }
            else if (_wavePlayer.PlaybackState == PlaybackState.Paused)
            {
                _wavePlayer.Play();
            }


        }
        public void StopAudio()
        {
           
            StopAudioInternal();

            

        }
        private void StopAudioInternal()
        {
            if(_currentAudio != null)
            {
                _mixingSampleProvider.RemoveMixerInput(_currentAudio);
                _currentAudioFile?.Dispose();
                _currentAudioFile = null!;

                _currentAudio = null!;
            }
        }

        public async Task shutDown() 
        {
           

            _wavePlayer?.Stop();

            

            await Task.Delay(200);

           
          
            if (_wavePlayer != null)
            {
                _wavePlayer.Dispose();
                _wavePlayer = null!;
            }
            
        }
        public double GetCurrentTime()
        {
            if (_currentAudioFile is not null)
            {
                return _currentAudioFile.CurrentTime.TotalSeconds;
            }
            return 0;
        }

       
       
    }
}
