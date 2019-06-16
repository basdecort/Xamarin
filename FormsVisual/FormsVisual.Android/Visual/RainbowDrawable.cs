using System;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace FormsVisual.Droid.Visual
{
    public class RainbowDrawable 
    {
        public static Drawable Get()
        {
            var colors = new int[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.LightBlue, Color.Blue, Color.Purple };
            return new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors);
        }
    }
}
