using System;
using SkiaSharp;

namespace DrawIt
{
    public static class ColorFills
    {

        public static SKPaint blackLine = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Stroke,
            StrokeWidth = 10,
            Color = SKColors.Black
        };

        public static SKPaint orangeFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.OrangeRed
        };

        public static SKPaint lawnGreenFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.LawnGreen
        };

        public static SKPaint skyBlueFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.SkyBlue
        };

        public static SKPaint sunFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.Yellow
        };

        public static SKPaint houseFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.Beige
        };

        public static SKPaint roadFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.DarkSlateGray
        };

        public static SKPaint brownFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.SaddleBrown
        };

        public static SKPaint greenFill = new SKPaint
        {
            Style = SkiaSharp.SKPaintStyle.Fill,
            Color = SKColors.DarkGreen
        };
    }
}
