using CoreGraphics;
using FormsVisual.iOS.Extensions;
using FormsVisual.iOS.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Picker), typeof(AwesomePickerRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.iOS.Visual
{
    public class AwesomePickerRenderer : PickerRenderer
    {
        public AwesomePickerRenderer()
        {
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            Control.Layer.AddRainbowSubLayer(rect);
        }
    }
}
