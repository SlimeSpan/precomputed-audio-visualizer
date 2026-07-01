using System;
using System.Collections.Generic;
using System.Text;

namespace sound_visualization
{
    internal interface IRenderChannel
    {
        
        public bool Enable { get; set; }
        public void UpdateSize(float allowedComponetsHeight, float allowedComponentwidth, float baseline);
        public void UpdateAndDraw(Graphics g, ReadOnlySpan<float> values);

        public void SetColor(Color[] colors);
    }
}
