
using System.Diagnostics;

namespace Filter.Core
{
    public class LowPassFilter:FilterBase
    {
        
        private float _alpha;
        public float Alpha
        {
            get => _alpha;
            set
            {
                Debug.Assert(_alpha>=0f|| _alpha<=1f, "illegal alpha value");
                Math.Clamp(value, 0f, 1f);
            }
        }
       

        public LowPassFilter(float alpha, int bandCount):base(bandCount) 
        {
            if (alpha < 0 || alpha > 1)
            {
                throw new ArgumentOutOfRangeException("alpha must between 0 and 1");
            }
            this._alpha = alpha;

            
           
            
        }
        public LowPassFilter(float cutoffFrequency, float sampleRate,int bandCount):base(bandCount)
        {

            _alpha = GetCoefficient(cutoffFrequency, sampleRate);

           
        } 

       
       
        public override ReadOnlySpan<float> Process(ReadOnlySpan<float> inputSpan)
        {
            
            if (inputSpan.Length != Buffer.Length)
                throw new ArgumentException("Input spectrum size mismatch.");

            for (int i = 0; i < inputSpan.Length; i++)
            {

                Buffer[i] += _alpha * (inputSpan[i] - Buffer[i]);
            }

            return Buffer;
        }
      

    }
}
 