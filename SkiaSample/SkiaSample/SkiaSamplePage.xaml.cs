using SkiaSharp;
using Xamarin.Forms;

namespace SkiaSample
{
    public partial class SkiaSamplePage : ContentPage
    {
        public SkiaSamplePage()
        {
            InitializeComponent();
        }

        void Handle_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.DrawCircle(0,0,100, new SKPaint{ Style = SkiaSharp.SKPaintStyle.Fill, Color = SKColors.Black });
        }
    }
}
