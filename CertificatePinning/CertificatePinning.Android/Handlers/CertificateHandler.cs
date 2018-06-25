using System.Collections.Generic;
using Java.Security;
using Java.Security.Cert;
using Javax.Net.Ssl;
using Xamarin.Android.Net;

namespace CertificatePinning.Droid.Handlers
{
    public class CertificateHandler : AndroidClientHandler
    {
        private Dictionary<string, string> _supported = new Dictionary<string, string>
        {
            {"Xamarin.com","xamarin.cer"},
            {"MicrosoftIT", "microsoftit.cer"},
            {"Cloudfront.net","cloudfront.cer"},
            {"Google", "googleapis.cer"},
            {"GoogleAnalytics", "google-analytics.cer"}
        };

        private TrustManagerFactory _trustManagerFactory;
        private KeyManagerFactory _keyManagerFactory;
        private KeyStore _keyStore;

        protected override TrustManagerFactory ConfigureTrustManagerFactory(KeyStore keyStore)
        {
            if (_trustManagerFactory != null)
            {
                return _trustManagerFactory;
            }

            _trustManagerFactory = TrustManagerFactory
                .GetInstance(TrustManagerFactory.DefaultAlgorithm);

            _trustManagerFactory.Init(keyStore);
            return _trustManagerFactory;
        }

        protected override KeyManagerFactory ConfigureKeyManagerFactory(KeyStore keyStore)
        {
            if (_keyManagerFactory != null)
            {
                return _keyManagerFactory;
            }

            _keyManagerFactory = KeyManagerFactory
                .GetInstance(KeyManagerFactory.DefaultAlgorithm);

            _keyManagerFactory.Init(keyStore, null);

            return _keyManagerFactory;
        }

        protected override KeyStore ConfigureKeyStore(KeyStore keyStore)
        {
            if (_keyStore != null)
            {
                return _keyStore;
            }

            _keyStore = KeyStore.GetInstance(KeyStore.DefaultType);
            _keyStore.Load(null, null);

            CertificateFactory cff = CertificateFactory.GetInstance("X.509");

            foreach (var cer in _supported)
            {
                Java.Security.Cert.Certificate cert;

                // Add your Certificate to the Assets folder and address it here by its name
                using (var certStream = Android.App.Application.Context.Assets.Open(cer.Value))
                {
                    cert = cff.GenerateCertificate(certStream);
                }

                _keyStore.SetCertificateEntry(cer.Key, cert);
            }

            return _keyStore;
        }
    }
}
