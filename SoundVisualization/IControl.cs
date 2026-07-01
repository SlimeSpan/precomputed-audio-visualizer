
namespace sound_visualization
{
    internal interface IControl
    {
        public void UpdateSize(float allowedComponetsHeight, float allowedComponentwidth, float baseline);
        public void UpdateAndDraw(Graphics g, ReadOnlySpan<float> values);

        public void SetColor(Color[] colors);

     
        

        
    }
}
