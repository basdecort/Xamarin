using CertificatePinning;
using CertificatePinning.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CertificatePinning.Services;
using System;

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

        /// <summary>
        /// This will for all loaded URLs
        /// </summary>
        /// <returns><c>true</c>, if start load was shoulded, <c>false</c> otherwise.</returns>
        /// <param name="webView">Web view.</param>
        /// <param name="request">Request.</param>
        /// <param name="navigationType">Navigation type.</param>
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
 
        /// <summary>
        /// This will be triggered for all links inside WebView
        /// </summary>
        /// <param name="connection">Connection.</param>
        /// <param name="challenge">Challenge.</param>
      /*  [Export("connection:willSendRequestForAuthenticationChallenge:")]
        public void WillSendRequestForAuthenticationChallenge(NSUrlConnection connection, NSUrlAuthenticationChallenge challenge)
        {

            if (challenge.ProtectionSpace.AuthenticationMethod == NSUrlProtectionSpace.AuthenticationMethodServerTrust)
            {
                var trust = challenge.ProtectionSpace.ServerSecTrust;
                var result = trust.Evaluate();
                bool trustedCertificate = result == SecTrustResult.Proceed || result == SecTrustResult.Unspecified;
               
                if (trustedCertificate)
                {
                    challenge.Sender.UseCredential(new NSUrlCredential(trust), challenge);
                }
                else
                {
                    Console.WriteLine("Rejecting request");
                    challenge.Sender.CancelAuthenticationChallenge(challenge);


                    return;
                }
            }*/
        }
    }
}
