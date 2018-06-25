using System;
using Xamarin.Forms.Platform.Android;
using Android.Webkit;
using CertificatePinning;
using CertificatePinning.Droid.Renderers;
using Xamarin.Forms;
using Android.Content;
using CertificatePinning.Services;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace CertificatePinning.Droid.Renderers
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        public CustomWebViewRenderer(Context ctx) : base(ctx){}

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            Control.SetWebViewClient(new SafeWebViewClient());
        }
    }

    public class SafeWebViewClient : Android.Webkit.WebViewClient
    {
        public SafeWebViewClient() : base()
        {

        }

        public override WebResourceResponse ShouldInterceptRequest(Android.Webkit.WebView view, IWebResourceRequest request)
        {
            var url = request.Url;

            try
            {
                var response = new SafeService().GetResponse(url.ToString()).Result;

                var contentType = response.Content.Headers.ContentType;
                if (contentType != null)
                {

                    return new WebResourceResponse(contentType.MediaType, contentType.CharSet, response.Content.ReadAsStreamAsync().Result);
                }

            }
            catch (Exception ex)
            {
                return new WebResourceResponse(null, null, null);
            }
            return base.ShouldInterceptRequest(view, request);
        }
    }
}
