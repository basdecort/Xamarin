using CoreGraphics;
using FormsVisual.iOS.Extensions;
using FormsVisual.iOS.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Frame), typeof(AwesomeFrameRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.iOS.Visual
{
    public class AwesomeFrameRenderer : FrameRenderer
    {
        public AwesomeFrameRenderer()
        {
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            NativeView.Layer.AddRainbowSubLayer(rect);
        }
    }
}
