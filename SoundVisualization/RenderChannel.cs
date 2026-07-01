using SignalVisualizer.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace sound_visualization
{
    internal class RenderChannel<T>:IRenderChannel
    {
        SignalVisualizerBase<T> _signalVisualizer;
        DrawGradientBase<T> _draw;

        public bool Enable { get; set; } = true;
        public RenderChannel(SignalVisualizerBase<T> signalVisualizer, DrawGradientBase<T> draw)
        {

           
            _signalVisualizer = signalVisualizer;
            _draw = draw;
        }

        public void UpdateSize(float allowedComponetsHeight, float allowedComponentwidth, float baseline)
        {
            
            _signalVisualizer.MaxComponentWidth = allowedComponentwidth;
            _signalVisualizer.MaxComponentHeight = allowedComponetsHeight;
            _signalVisualizer.BaseLineHeight = baseline;





        }
        public void UpdateAndDraw(Graphics g, ReadOnlySpan<float> values)
        {
            if (!Enable)
            {
                return;
            }

            ReadOnlySpan<T> drawData = _signalVisualizer.GetDatas(values); //get visualized datas for drawing

            _draw.Draw(g, drawData);


        }
        public void SetColor(Color[] colors)
        {
            _draw.Colors = colors;
        }
    }
}
