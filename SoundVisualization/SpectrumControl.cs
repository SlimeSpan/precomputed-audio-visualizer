using System;
using System.Collections.Generic;
using System.Text;

namespace sound_visualization
{
    class SpectrumControl:Control
    {
        public event Action<Graphics>? Render;
        
        public SpectrumControl()
        {
            SetStyle(
            
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer,
            true);
            this.DoubleBuffered = true;
            ResizeRedraw = false;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            Render?.Invoke(e.Graphics);
        }
      
    }
}
