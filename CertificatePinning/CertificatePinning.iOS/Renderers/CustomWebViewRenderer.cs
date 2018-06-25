using CertificatePinning;
using CertificatePinning.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using WebKit;
using CertificatePinning.Services;
using System;
using Security;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace CertificatePinning.iOS.Renderers
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            Delegate = new SafeWebViewDelegate(this);
        }
    }

    public class SafeWebViewDelegate : UIWebViewDelegate, INSUrlConnectionDelegate
    {
  
        private readonly CustomWebViewRenderer _renderer;
        public SafeWebViewDelegate(CustomWebViewRenderer customWebViewRenderer)
        {
            _renderer = customWebViewRenderer;
        }

        [Export("webView:shouldStartLoadWithRequest:navigationType:")]
        public bool ShouldStartLoad(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
        {
            if (request.Url.Scheme.ToLower() == "https")
            {
                try
                {
                    var response = new SafeService().GetResponse(request.Url.ToString()).Result;

                    var conn = new NSUrlConnection(request, this, true);
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }

            return false;
        }
 
        [Export("connection:willSendRequestForAuthenticationChallenge:")]
        public void WillSendRequestForAuthenticationChallenge(NSUrlConnection connection, NSUrlAuthenticationChallenge challenge)
        {

            if (challenge.ProtectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodServerTrust)
            {
                var trust = challenge.ProtectionSpace.ServerSecTrust;
                var result = trust.Evaluate();
                bool trustedCertificate = result == SecTrustResult.Proceed || result == SecTrustResult.Unspecified;
                Console.WriteLine(trustedCertificate);

                if (trustedCertificate)
                {
                    challenge.Sender.PerformDefaultHandling(challenge);
                }
               /* if (!trustedCertificate && trust.Count != 0)
                {
                    var originalCertificate = trust[0].ToX509Certificate2();
                    var x509Certificate = new Certificate(challenge.ProtectionSpace.Host, originalCertificate);
                    trustedCertificate = _renderer.Element.ShouldTrustUnknownCertificate(x509Certificate);
                }

                if (trustedCertificate)
                {
                    challenge.Sender.UseCredential(new NSUrlCredential(trust), challenge);
                }
                else
                {
                    Console.WriteLine("Rejecting request");
                    challenge.Sender.CancelAuthenticationChallenge(challenge);


                    return;
                }*/
            }
        }
    }
}
