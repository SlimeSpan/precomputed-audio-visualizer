using SignalVisualizer.Core;

namespace sound_visualization
{
    internal class WinformControl:IControl
    {
        List<IRenderChannel> _signalVisualizer;
       
        public WinformControl()
        {
            _signalVisualizer = new List<IRenderChannel>();
           
        }
        public void AddRenderChannel(IRenderChannel channel)
        {
            _signalVisualizer.Add(channel);
        }
        public void UpdateSize(float allowedComponetsHeight,float allowedComponentwidth,float baseline)
        {
            for (int i = 0; i <_signalVisualizer.Count; i++)
            {
                _signalVisualizer[i].UpdateSize(allowedComponetsHeight, allowedComponentwidth, baseline);
            }
           
        }
        public void UpdateAndDraw(Graphics g,ReadOnlySpan<float> values)
        {

            for (int i = 0; i < _signalVisualizer.Count; i++)
            {
                _signalVisualizer[i].UpdateAndDraw(g, values);
            }
           
            
        }
        public void SetColor(Color[] colors)
        {
            for (int i = 0; i < _signalVisualizer.Count; i++)
            {
                _signalVisualizer[i].SetColor(colors);
            }
        }
       
    }
}
