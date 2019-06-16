using FormsVisual.iOS.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ActivityIndicator), typeof(AwesomeActivityIndicatorRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.iOS.Visual
{
    public class AwesomeActivityIndicatorRenderer : ActivityIndicatorRenderer
    {
        public AwesomeActivityIndicatorRenderer()
        {
        }
    }
}
