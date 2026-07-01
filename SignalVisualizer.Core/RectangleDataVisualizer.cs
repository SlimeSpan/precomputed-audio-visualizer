

using System.Diagnostics;
using System.Drawing;

namespace SignalVisualizer.Core
{
    public class RectangleDataVisualizer:SignalVisualizerBase<RectangleF>
    {  
        
       
        private float _barWidthRatio;
        public float BarWidthRatio 
        {   get => _barWidthRatio; 
            set
            {
                Debug.Assert(value >=0&&value<=1,"illegal barWidthRation value ");
                
                _barWidthRatio = Math.Clamp(value,0,1);
            }
        }
        public float MinBarWidth { get; set; } = 0;
        public float MinBarHeight { get; set; } = 0;



        public RectangleDataVisualizer(float maxHeight, float maxWidth, float baseLineHeight,int bandCount,float barWidthRatio) :base(maxHeight,maxWidth,baseLineHeight,bandCount)
        {
            if(!(barWidthRatio>=0&&barWidthRatio<=1))
            {
                throw new ArgumentOutOfRangeException(nameof(barWidthRatio));
            }
            _barWidthRatio = barWidthRatio;
           
        }
        public override ReadOnlySpan<RectangleF> GetDatas(ReadOnlySpan<float> currentFrameBands)
        {

            EnsureCapacity(currentFrameBands);

            float totalWidth = MaxComponentWidth/currentFrameBands.Length;

            float barWidth = totalWidth * _barWidthRatio;
            barWidth = MathF.Max(barWidth, 1f);
            float barSpacing = totalWidth * (1 - _barWidthRatio);
            
            for (int i = 0; i < currentFrameBands.Length; i++)
            {

                float x = (float)i * (barWidth + barSpacing);
                float height = currentFrameBands[i] * MaxComponentHeight;
                float y = BaseLineHeight- height;

                height = MathF.Max(height, 1f);

                Datas[i] = new RectangleF(x, y,barWidth,height);
            }
            return Datas.AsSpan(0,currentFrameBands.Length);
        }



    }
}
