using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaSample
{
    public partial class SkiaSamplePage : ContentPage
    {

        
        SKPaint blackLine = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Stroke,
            StrokeWidth = 10,
            Color = SKColors.Black
        };

        SKPaint orangeFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.OrangeRed
        };

        SKPaint lawnGreenFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.LawnGreen
        };

        SKPaint skyBlueFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.SkyBlue
        };

        SKPaint sunFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.Yellow
        };

        SKPaint houseFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.Beige
        };

        SKPaint roadFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.DarkSlateGray
        };

        SKPaint brownFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.SaddleBrown
        };

        SKPaint greenFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.DarkGreen
        };

        public SkiaSamplePage()
        {
            InitializeComponent();
    
        }

        void Handle_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            // This will cause 0,0 to be at the center
            var width = e.Info.Width;
            var height = e.Info.Height;
            canvas.Translate(width / 2, height / 2);

            // Grass
            canvas.DrawRect(new SKRect(-(width / 2), -200, height, width), lawnGreenFill);

            // Sky
            canvas.DrawRect(new SKRect(-(width / 2),-(height/2),width, -200), skyBlueFill);

            // Sun
            var y = -(height / 2) + 150;
            canvas.DrawCircle(200, y, 30, sunFill);

            // Sun stripes
            for (int degrees = 0; degrees < 360; degrees += 30)
            {
                // This will save canvas state
                canvas.Save();
                canvas.RotateDegrees(degrees, 200, y);
                canvas.DrawRect(new SKRect(200, y - 80, 204, y - 40), sunFill);
                // This will restore the saved canvas state so the rotation doenst impact next draw calls
                canvas.Restore();
            }

            // Walls
            canvas.DrawRect(new SKRect(0, -150, 300, 150), houseFill);
            canvas.DrawRect(new SKRect(0, -150, 300, 150), blackLine);

            // Window
            canvas.DrawCircle(150, -30, 30, blackLine);

            // Cross in window
            canvas.DrawLine(120, -30, 180, -30, blackLine);
            canvas.DrawLine(150, -60, 150, 0, blackLine);
                  
            // Door
            canvas.DrawRect(new SKRect(115, 50, 185, 150), blackLine);

            // Roof
            SKPath path = new SKPath();
            path.MoveTo(0, -150);
            path.LineTo(150, -300);
            path.LineTo(300, -150);
            path.LineTo(0, -150);
            path.Close();
            canvas.DrawPath(path, orangeFill);
            canvas.DrawPath(path, blackLine);

            // Road
            canvas.DrawRect(new SKRect(- (width/2), 300, width,560), roadFill);
            var stipeWidth = 60;
            for (var i = (-(width / 2)); i < width; i += 40)
            {
                canvas.DrawRect(new SKRect(i, 425, i+stipeWidth, 435), sunFill);
                i = i + stipeWidth;
            }

            // Tree
            canvas.DrawRect(new SKRect(-250, 0, -210, 140), brownFill);
            canvas.DrawCircle(-260, 0, 30, greenFill);
            canvas.DrawCircle(-210, 0, 50, greenFill);
            canvas.DrawCircle(-180, 0, 20, greenFill);
            canvas.DrawCircle(-230, -40, 40, greenFill);
        }
    }
}
