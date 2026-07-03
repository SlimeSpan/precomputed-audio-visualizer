using System;
using System.Collections.Generic;
using System.Text;

namespace sound_visualization
{

    public class TransparentPanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
           
        }
    }


}
