using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using ModernHttpClient;
using System.Net.Security;

namespace CertificatePinning.Services
{
    public class SafeService
    {
        public static HttpClient HttpClient;

        private static Dictionary<string,string> _allowedPublicKeys = new Dictionary<string, string>()
        {
            {"Xamarin.com","3082010A0282010100CDF9AEF37687F63E3818D346013F8861E65C272C034FF698A242120D1E6286D97994C2035EC4BE90B1C8DE3B8ECD8E56861C0C7A0C00754CAFF06520479CFA638C4FB970702AC1E55A2793FD25EC348877A65A405EC2135B5C8C70027D5D6879773649BFBCD115DA0C390F44168CE0A52F986D0DFD33282A8DAAB04A9506E859DDF307C85C028DE01DB868164FE1A3B1D788C6B7C5D4ED3CEABE8C91390C83E300776C1A7E2503DDBE8B9EEBEB27FF9CF00BB3D894BF3886032A8085448C21DF69E50DB64F4A481760A683074E3D5B2A3C4E383563551912273F94067111F7FB700AEDC3C446300C7784AF08DFFE94984805F00952CD3B56574B2A1B993898050203010001"},
            {"Nu.nl", "3082010A0282010100C738913B2A1A270CA18353178F58FD7C9A4C37E169C48E1E2060D190AB7ADEADD7F2231A0D5F3B23D0F71967C448A51E28D9385D1C9107AA4EC45E0A3DA938FFEF5C2BC228EEA733662C67ADA92617D1B93BC5694ACBCCB54A27973466D63FCF09D02CB8407535B550B2F68F4F2B33A413D47ABD12D198ABCE86553003CD36E980B968179DFF92A7E9C5E47ACBCDD6B3DAD209BD20F4B1D57345E307EEDA65D722C12BC348A4E90D6ADD6AC1C8DEF19AAE84DA21D748754D98A5FA9A083C0FF6CC274F486F6F0241D76487B56B35E9B2580B12678E15A843E6A10BB1813C05FA3260BC8A1CA4DDEA86F2EAD1D69925C32C30743842E5A06EB0BC7C13A588A2110203010001"}
        };

        public static HttpClient CreateClient(HttpClientHandler handler =null)
        {
            InitCertificatePinning();

      //      var key = GetPubKey("/Users/gebruiker/Desktop/nu.cer");

            return handler == null ? new HttpClient() : new HttpClient(handler);
        }


        /*private HttpClient CreateNativeHttpClient()
        {
            InitCertificatePinning();
            return new HttpClient(Handler);
        }*/

        private HttpClient CreateModernHttpClient()
        {
            InitCertificatePinning();
            return new HttpClient(new NativeMessageHandler(
                            throwOnCaptiveNetwork: false,
                            customSSLVerification: true
                            ));
        }

        private static void InitCertificatePinning()
        {
            ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
        }

        /// <summary>
        /// Validates the certificate.
        /// </summary>
        /// <returns><c>true</c>, if certificate was validated, <c>false</c> otherwise.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="certificate">Certificate.</param>
        /// <param name="chain">Certificate chain sent by server.</param>
        /// <param name="sslPolicyErrors">Errors like name mismatch.</param>
        private static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return _allowedPublicKeys.Values.Contains(certificate?.GetPublicKeyString());
        }

        /// <summary>
        /// Validates the certificate.
        /// </summary>
        /// <returns><c>true</c>, if certificate was validated, <c>false</c> otherwise.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="certificate">Certificate.</param>
        /// <param name="chain">Certificate chain sent by server.</param>
        /// <param name="sslPolicyErrors">Errors like name mismatch.</param>
        private static bool ValidateIntermediate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Building the chain is required for iOS: https://bugzilla.xamarin.com/show_bug.cgi?id=7245
            X509Certificate2 c2 = new X509Certificate2(certificate.GetRawCertData());
            var builtChain = chain.Build(c2);

            if (chain.ChainElements.Count < 2)
            {
                // No Intermediate found
                return false;
            }
            var intermediateCertificate = chain.ChainElements[1].Certificate;
        
            return _allowedPublicKeys.Values.Contains(intermediateCertificate?.GetPublicKeyString());
        }

        public static string GetPubKey(string certFile)
        {
            var cert = X509Certificate.CreateFromCertFile(certFile);
            return cert.GetPublicKeyString();
        }

        public Task<string> GetContents(string uri)
        {
            try
            {
                return HttpClient.GetStringAsync(uri);
            }catch (Exception ex)
            {
                var aa = ex;
                return null;
            }
        }

        public Task<HttpResponseMessage> GetResponse(string uri)
        {
            return HttpClient.GetAsync(uri);
        }
    }
}
