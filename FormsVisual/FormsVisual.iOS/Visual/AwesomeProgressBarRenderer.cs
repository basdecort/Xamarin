using CoreGraphics;
using FormsVisual.iOS.Extensions;
using FormsVisual.iOS.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ProgressBar), typeof(AwesomeProgressBarRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.iOS.Visual
{
    public class AwesomeProgressBarRenderer : ProgressBarRenderer
    {
        public AwesomeProgressBarRenderer()
        {
        }
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            Control.Layer.AddRainbowSubLayer(rect);
        }
    }
}
