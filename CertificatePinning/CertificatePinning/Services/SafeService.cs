using System;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using System.IO;

namespace CertificatePinning.Services
{
    public class SafeService
    {
        public static HttpClient HttpClient;

        public static HttpClient CreateClient(HttpClientHandler handler =null)
        {
            InitCertificatePinning();
     
            return handler == null ? new HttpClient() : new HttpClient(handler);
        }

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
            CertficateValidation.Init();
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

        public Task<Stream> GetStream(string uri)
        {
            try
            {
                return HttpClient.GetStreamAsync(uri);
            }catch(Exception ex)
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetResponse(string uri)
        {
            return HttpClient.GetAsync(uri);
        }
    }
}
