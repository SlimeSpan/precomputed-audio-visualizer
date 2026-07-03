using System.Diagnostics;

namespace Filter.Core
{
    public class EnvelopeFollower:FilterBase
    {
        private float _attack;
        
        public float Attack
        {
            get => _attack;
            set
            {
                Debug.Assert(value>=0f||value<=1f,"illegal attack value");
                _attack = Math.Clamp(value, 0, 1);
            }
                
        }

        private float _release;
        public float Release
        {
            get=>_release;
            set
            {
                Debug.Assert(value >= 0f || value <= 1f, "illegal decay value");
                _release = Math.Clamp(value, 0, 1);
            }
        }

   
        public EnvelopeFollower(float attack,float release,int bandCount):base(bandCount)  
        {
            if(attack<0||attack>1)
            {
                throw new ArgumentOutOfRangeException(nameof(attack));
            }
            if(release<0||release>1)
            {
                throw new ArgumentOutOfRangeException();
            }

            _attack = attack;
            _release = release;
            

        }
        public EnvelopeFollower(float attackCutoffFrequency,float releaseCutoffFrequency,float sampleRate,int bandCount):base(bandCount)
        {
           

            _attack = GetCoefficient(attackCutoffFrequency, sampleRate);
            _release = GetCoefficient(releaseCutoffFrequency, sampleRate);

        }
        
        public override  ReadOnlySpan<float> Process(ReadOnlySpan<float> inputs)
        {

            if(Buffer.Length!=inputs.Length)
            {
                throw new ArgumentException($"input length:{inputs.Length} and buffer length:{Buffer.Length} mismatch");
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                float prev = Buffer[i];
                float input = inputs[i];
                
                float k = prev>input? _release:_attack;
               Buffer[i] = prev + k * (input - prev);
            }
            return Buffer;

        }
    }
}
