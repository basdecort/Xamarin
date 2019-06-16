using Android.Content;
using FormsVisual.Droid.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Picker), typeof(AwesomePickerRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.Droid.Visual
{
    public class AwesomePickerRenderer: PickerRenderer
    {
        public AwesomePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            Control.SetBackground(RainbowDrawable.Get());
        }
    }
}
