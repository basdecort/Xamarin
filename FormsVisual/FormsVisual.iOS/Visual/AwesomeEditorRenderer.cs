using CoreGraphics;
using FormsVisual.iOS.Extensions;
using FormsVisual.iOS.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Editor), typeof(AwesomeEditorRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.iOS.Visual
{
    public class AwesomeEditorRenderer : EditorRenderer
    {
        public AwesomeEditorRenderer()
        {
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            Control.Layer.AddRainbowSubLayer(rect);
        }
    }
}
