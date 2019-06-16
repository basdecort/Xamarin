using Android.Content;
using FormsVisual.Droid.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Stepper), typeof(AwesomeStepperRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.Droid.Visual
{
    public class AwesomeStepperRenderer : StepperRenderer
    {
        public AwesomeStepperRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Stepper> e)
        {
            base.OnElementChanged(e);
            Control.SetBackground(RainbowDrawable.Get());
        }
    }
}
