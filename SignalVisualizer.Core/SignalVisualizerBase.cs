
namespace SignalVisualizer.Core
{
    
    public abstract class SignalVisualizerBase<T>
    {
        public float MaxComponentHeight { get; set; }
        public float MaxComponentWidth { get; set; }

        public float BaseLineHeight { get; set; }

        protected T[] Datas;
        public int Capacity
        {
            get => Datas.Length;
            set
            {
                if (value <= 0)
                {
                    Datas = Array.Empty<T>();
                }
                else
                {
                    Datas = new T[value];
                }
                
            }
        }
        public float currentMax = 0;
        public SignalVisualizerBase(float maxHeight,float maxWidth,float baseLineHeight,int bandCount) 
        {
            
           
            this.MaxComponentHeight = maxHeight;
            this.BaseLineHeight= baseLineHeight;
            this.MaxComponentWidth = maxWidth;
            
            Datas  = new T[bandCount];
            
        }
        public abstract ReadOnlySpan<T> GetDatas(ReadOnlySpan<float> value);
        protected void EnsureCapacity(ReadOnlySpan<float> value)
        {
            if(Datas.Length<value.Length)
            {
                Datas = new T[value.Length];
            }
        }
        
        
    }
}
