using CoreGraphics;
using FormsVisual.iOS.Extensions;
using FormsVisual.iOS.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.TimePicker), typeof(AwesomeTimePickerRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.iOS.Visual
{
    public class AwesomeTimePickerRenderer : TimePickerRenderer
    {
        public AwesomeTimePickerRenderer()
        {
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            Control.Layer.AddRainbowSubLayer(rect);
        }
    }
}
