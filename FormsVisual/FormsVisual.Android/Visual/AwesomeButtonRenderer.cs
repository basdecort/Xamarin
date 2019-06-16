using Android.Content;
using FormsVisual.Droid.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(AwesomeButtonRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.Droid.Visual
{
    public class AwesomeButtonRenderer : ButtonRenderer
    {
        public AwesomeButtonRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Control.SetBackground(RainbowDrawable.Get());
        }
    }
}