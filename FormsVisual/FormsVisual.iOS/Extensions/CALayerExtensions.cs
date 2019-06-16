using System;
using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace FormsVisual.iOS.Extensions
{
    public static class CALayerExtensions
    {
        public static void AddRainbowSubLayer(this CALayer layer, CGRect rect)
        {
            CAGradientLayer gradient = new CAGradientLayer();
            gradient.Frame = rect;
            gradient.Colors = new CoreGraphics.CGColor[] { Color.Red.ToCGColor(), Color.Orange.ToCGColor(), Color.Yellow.ToCGColor(), Color.Green.ToCGColor(), Color.LightBlue.ToCGColor(), Color.Blue.ToCGColor(), Color.Purple.ToCGColor() };
            layer.InsertSublayer(gradient, 0);
        }

    }
}
