using System;
using System.Collections.Generic;

using Xamarin.Forms;

using TouchTracking;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using DrawIt;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using DrawIt.Services;
using Newtonsoft.Json;

namespace SkiaSharpFormsDemos.Paths
{
    public partial class FingerPaintPage : ContentPage
    {
        Dictionary<long, FingerPaintPolyline> inProgressPolylines = new Dictionary<long, FingerPaintPolyline>();
        List<FingerPaintPolyline> completedPolylines = new List<FingerPaintPolyline>();

        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };

        private ComputerVisionService _computerVisionService;

        public FingerPaintPage()
        {
            InitializeComponent();
            _computerVisionService = new ComputerVisionService();
        }

        void OnGuessButtonClicked(object sender, EventArgs args)
        {
            _takeSnapShot = true;

            canvasView.InvalidateSurface();
        }

        void OnClearButtonClicked(object sender, EventArgs args)
        {
            completedPolylines.Clear();
            canvasView.InvalidateSurface();
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!inProgressPolylines.ContainsKey(args.Id))
                    {
                        Color strokeColor = (Color)typeof(Color).GetRuntimeField(colorPicker.Items[colorPicker.SelectedIndex]).GetValue(null);
                        float strokeWidth = ConvertToPixel(new float[] { 1, 2, 5, 10, 20 }[widthPicker.SelectedIndex]);

                        FingerPaintPolyline polyline = new FingerPaintPolyline
                        {
                            StrokeColor = strokeColor,
                            StrokeWidth = strokeWidth
                        };
                        polyline.Path.MoveTo(ConvertToPixel(args.Location));

                        inProgressPolylines.Add(args.Id, polyline);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Moved:
                    if (inProgressPolylines.ContainsKey(args.Id))
                    {
                        FingerPaintPolyline polyline = inProgressPolylines[args.Id];
                        polyline.Path.LineTo(ConvertToPixel(args.Location));
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                    if (inProgressPolylines.ContainsKey(args.Id))
                    {
                        completedPolylines.Add(inProgressPolylines[args.Id]);
                        inProgressPolylines.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Cancelled:
                    if (inProgressPolylines.ContainsKey(args.Id))
                    {
                        inProgressPolylines.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        private bool _takeSnapShot;
        private SKImage _image;

        async void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;

            canvas.Clear();

            foreach (FingerPaintPolyline polyline in completedPolylines)
            {
                paint.Color = polyline.StrokeColor.ToSKColor();
                paint.StrokeWidth = polyline.StrokeWidth;
                canvas.DrawPath(polyline.Path, paint);
            }

            foreach (FingerPaintPolyline polyline in inProgressPolylines.Values)
            {
                paint.Color = polyline.StrokeColor.ToSKColor();
                paint.StrokeWidth = polyline.StrokeWidth;
                canvas.DrawPath(polyline.Path, paint);
            }

            PaintHouse(args);

            if (_takeSnapShot)
            {
                _image = args.Surface.Snapshot();
                _takeSnapShot = false;
                await Analyse();
            }
        }

        private void PaintHouse(SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            // This will cause 0,0 to be at the center
            var width = e.Info.Width;
            var height = e.Info.Height;
            canvas.Translate(width / 2, height / 2);

            // Grass
            canvas.DrawRect(new SKRect(-(width / 2), -200, height, width), ColorFills.lawnGreenFill);

            // Sky
            canvas.DrawRect(new SKRect(-(width / 2), -(height / 2), width, -200), ColorFills.skyBlueFill);

            // Sun
            var y = -(height / 2) + 150;
            canvas.DrawCircle(200, y, 30, ColorFills.sunFill);

            // Sun stripes
            for (int degrees = 0; degrees < 360; degrees += 30)
            {
                // This will save canvas state
                canvas.Save();
                canvas.RotateDegrees(degrees, 200, y);
                canvas.DrawRect(new SKRect(200, y - 80, 204, y - 40), ColorFills.sunFill);
                // This will restore the saved canvas state so the rotation doenst impact next draw calls
                canvas.Restore();
            }

            // Walls
            canvas.DrawRect(new SKRect(0, -150, 300, 150), ColorFills.houseFill);
            canvas.DrawRect(new SKRect(0, -150, 300, 150), ColorFills.blackLine);

            // Window
            canvas.DrawCircle(150, -30, 30, ColorFills.blackLine);

            // Cross in window
            canvas.DrawLine(120, -30, 180, -30, ColorFills.blackLine);
            canvas.DrawLine(150, -60, 150, 0, ColorFills.blackLine);

            // Door
            canvas.DrawRect(new SKRect(115, 50, 185, 150), ColorFills.blackLine);

            // Roof
            SKPath path = new SKPath();
            path.MoveTo(0, -150);
            path.LineTo(150, -300);
            path.LineTo(300, -150);
            path.LineTo(0, -150);
            path.Close();
            canvas.DrawPath(path, ColorFills.orangeFill);
            canvas.DrawPath(path, ColorFills.blackLine);

            // Road
            canvas.DrawRect(new SKRect(-(width / 2), 300, width, 560), ColorFills.roadFill);
            var stipeWidth = 60;
            for (var i = (-(width / 2)); i < width; i += 40)
            {
                canvas.DrawRect(new SKRect(i, 425, i + stipeWidth, 435), ColorFills.sunFill);
                i = i + stipeWidth;
            }

            // Tree
            canvas.DrawRect(new SKRect(-250, 0, -210, 140), ColorFills.brownFill);
            canvas.DrawCircle(-260, 0, 30, ColorFills.greenFill);
            canvas.DrawCircle(-210, 0, 50, ColorFills.greenFill);
            canvas.DrawCircle(-180, 0, 20, ColorFills.greenFill);
            canvas.DrawCircle(-230, -40, 40, ColorFills.greenFill);
        }

        private async Task Analyse()
        {
             

            // encode the image (defaults to PNG)
            SKData encoded = _image.Encode();
            Microsoft.ProjectOxford.Vision.Contract.AnalysisResult result;
            // get a stream over the encoded data
            using(var stream = encoded.AsStream())
            {
                result = await _computerVisionService.GetAnalysisResult(stream);
            }

            if (result != null)
            {
                var json = JsonConvert.SerializeObject(result);
                await DisplayAlert("Result", json, "OK");
            }else{
                await DisplayAlert("Result", "An error occured, make sure the API key is valid for the BaseUrl.", "OK");
            }
        }

        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
        }

        float ConvertToPixel(float fl)
        {
            return (float)(canvasView.CanvasSize.Width * fl / canvasView.Width);
        }

    }
}
