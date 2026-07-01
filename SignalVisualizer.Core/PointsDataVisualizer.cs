
using System.Drawing;


namespace SignalVisualizer.Core
{
    public class PointsDataVisualizer : SignalVisualizerBase<PointF>
    {
       

        
        public PointsDataVisualizer(float maxHeight, float maxWidth, float baseLineHeight,int bandCount) : base(maxHeight, maxWidth, baseLineHeight,bandCount)
        {
           
        }
       
        public override ReadOnlySpan<PointF> GetDatas(ReadOnlySpan<float> value)
        {
            EnsureCapacity(value);
            int bandCount = (value.Length - 1); //start counting with 0 

            for (int i=0;i<value.Length;i++)
            {
                float x = (float)i/bandCount * MaxComponentWidth;
                float y = BaseLineHeight - value[i] * MaxComponentHeight; 

                Datas[i] = new PointF(x, y);
            }
            return Datas;
        }
    }
}

