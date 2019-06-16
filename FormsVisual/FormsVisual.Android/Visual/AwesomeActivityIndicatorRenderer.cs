using Android.Content;
using FormsVisual.Droid.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ActivityIndicator), typeof(AwesomeActivityIndicatorRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.Droid.Visual
{
    public class AwesomeActivityIndicatorRenderer : ActivityIndicatorRenderer
    {
        public AwesomeActivityIndicatorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<ActivityIndicator> e)
        {
            base.OnElementChanged(e);
        
            Control.SetBackground(RainbowDrawable.Get());
        }
    }
}
