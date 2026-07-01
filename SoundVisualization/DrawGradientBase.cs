using sound_visualization;
using System.Security.Cryptography.X509Certificates;

internal abstract class DrawGradientBase<T>
{
    private static readonly Color[] defaultColor = { Color.Red,Color.Blue};
    private Color[] _color = defaultColor;
    public  Color[] Colors
    {
        get => _color;
        set => _color = value;
    }
    //public float MaxHeight;
    //protected float BaseLineHeight;

    public abstract void Draw(Graphics g, ReadOnlySpan<T> data);

   
   
}