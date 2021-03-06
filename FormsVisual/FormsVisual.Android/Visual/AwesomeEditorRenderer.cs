﻿using Android.Content;
using FormsVisual.Droid.Visual;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Editor), typeof(AwesomeEditorRenderer), new[] { typeof(FormsVisual.Styles.AwesomeVisual) })]
namespace FormsVisual.Droid.Visual
{
    public class AwesomeEditorRenderer : EditorRenderer
    {
        public AwesomeEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            Control.SetBackground(RainbowDrawable.Get());
        }
    }
}
