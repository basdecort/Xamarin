using System;
using Xamarin.Forms;
using CertificatePinning.Services;
using System.Threading.Tasks;

namespace CertificatePinning.Views
{
    public class CustomCrossWebView : WebView
    {
        public async Task Open(string url)
        {
            if (url.ToLower().Contains("https:"))
            {
                var html = await new SafeService().GetContents(url);
                if (html != null)
                {
                    Source = new HtmlWebViewSource { Html = html };
                }
            }
        }
    }
}
