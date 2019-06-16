using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using FormsVisual.iOS.Extensions;
using FormsVisual.iOS.Visual;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(AwesomeButtonRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.iOS.Visual
{
    public class AwesomeButtonRenderer : ButtonRenderer
    {
        public AwesomeButtonRenderer()
        {
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            Control.Layer.AddRainbowSubLayer(rect);
        }
    }
}
