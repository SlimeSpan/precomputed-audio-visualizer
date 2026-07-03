
using System.Diagnostics;
using System.Drawing.Drawing2D;



namespace sound_visualization
{
    internal class DrawWave : DrawGradientBase<PointF>, IDisposable
    {

        private readonly GraphicsPath _path = new();

        public override void Draw(Graphics g, ReadOnlySpan<PointF> data)
        {

            if (data.IsEmpty || Colors.Length < 2)
                return;

            _path.Reset();
            Color c0 = Colors[0];
            Color c1 = Colors[1];

            float top = data[0].Y;
            float bottom = data[0].Y;
            float screenBottom = g.VisibleClipBounds.Bottom;
            g.SmoothingMode = SmoothingMode.HighQuality;
            foreach (var r in data)
            {
                top = MathF.Min(top, r.Y);
                bottom = MathF.Max(bottom, r.Y);
            }


            int lastIndex = data.Length - 1;
            _path.AddCurve(data);

            _path.AddLine(data[lastIndex].X, data[lastIndex].Y, data[lastIndex].X, screenBottom);
            _path.AddLine(data[lastIndex].X, screenBottom, data[0].X, screenBottom);

            _path.CloseFigure();
            RectangleF bound = _path.GetBounds();
           
            if (MathF.Abs(screenBottom - bound.Top) < 1)
            {
                return;
            }
            try
            {

                using (LinearGradientBrush brush = new LinearGradientBrush(new PointF(0, screenBottom),new PointF(0, bound.Top), c0, c1))
                {
                    g.FillPath(brush, _path);
                  
                   
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine("Fail to Draw Wave,{0}",ex.Message);
            }

        }
        public void Dispose()
        {
            _path.Dispose();
        }
       
        

    }
}
