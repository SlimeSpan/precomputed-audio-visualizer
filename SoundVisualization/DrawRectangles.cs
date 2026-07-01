
using NAudio.MediaFoundation;
using sound_visualization;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;
namespace SignalVisualizer.Core
{
    internal class DrawRectangles:DrawGradientBase<RectangleF>
    {

        public override void Draw(Graphics g, ReadOnlySpan<RectangleF> rectangles)
        {
            
                if (rectangles.IsEmpty || Colors.Length < 2)
                    return;

                var c0 = Colors[0];
                var c1 = Colors[1];

                float top = rectangles[0].Bottom;
                float bottom = rectangles[0].Top;

                foreach (var r in rectangles)
                {
                    top = MathF.Min(top, r.Top);
                    bottom = MathF.Max(bottom, r.Bottom);
                }

            try
            {
                using var brush = new LinearGradientBrush(
                    new PointF(0, bottom),
                    new PointF(0, top),
                    c0,
                    c1);
            

                g.FillRectangles(brush, rectangles);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fail to Draw Rectangle,{0}",ex.Message);
               
            }

        }

        
    }
}
