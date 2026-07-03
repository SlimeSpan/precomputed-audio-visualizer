

namespace Filter.Core
{
    public abstract class FilterBase
    {
        protected float[] Buffer;
       
        protected FilterBase(int bandCount) 
        {
            Buffer = new float[bandCount];
        }
        protected float GetCoefficient(float cutoffFrequency,float sampleRate)
        {
            if (cutoffFrequency <= 0 || sampleRate <= 0)
            {
                throw new ArgumentOutOfRangeException("CutoffFrequency and SampleRate must > 0");
            }

            float RC = 1.0f / (cutoffFrequency * 2.0f * (float)Math.PI);
            float dt = 1.0f / sampleRate;
            float result = dt / (RC + dt);

            if (result < 0 || result > 1)
            {
                throw new ArgumentOutOfRangeException("alpha must between 0 and 1");
            }
            return result;
        }
        public abstract ReadOnlySpan<float> Process(ReadOnlySpan<float> inputs);
        public void Reset()
        {
            if(Buffer is not null)
            {
                Array.Clear(Buffer, 0, Buffer.Length);
            }
        }
    }
}
