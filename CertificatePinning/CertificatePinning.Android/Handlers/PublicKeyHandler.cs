using System;
using Java.Security.Cert;
using Javax.Net.Ssl;
using System.Diagnostics;
using Xamarin.Android.Net;
using Java.Security;
using Java.Net;
using Android.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace CertificatePinning.Droid.Handlers
{

    /// <summary>
    /// Based on: https://github.com/jiahaoliuliu/pubKeyPinningInAndroid/blob/master/app/src/main/java/org/owasp/pubkeypin/PubKeyManager.java
    /// </summary>
    public class PublicKeyManager : Java.Lang.Object, IX509TrustManager 
    {
        public Dictionary<string, string> _allowedPubKeys = new Dictionary<string, string>
        {
            {"Xamarin.com",@"MIIGuTCCBKGgAwIBAgITFgAA1W8sGnYS39Xh7gAAAADVbzANBgkqhkiG9w0BAQsFADCBizELMAkG"+
"A1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoT"+
"FU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEVMBMGA1UECxMMTWljcm9zb2Z0IElUMR4wHAYDVQQDExVN"+
"aWNyb3NvZnQgSVQgVExTIENBIDQwHhcNMTgwMjEzMDExMzE5WhcNMjAwMjEzMDExMzE5WjAYMRYw"+
"FAYDVQQDDA0qLnhhbWFyaW4uY29tMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAzfmu"+
"83aH9j44GNNGAT+IYeZcJywDT/aYokISDR5ihtl5lMIDXsS+kLHI3juOzY5WhhwMegwAdUyv8GUg"+
"R5z6Y4xPuXBwKsHlWieT/SXsNIh3plpAXsITW1yMcAJ9XWh5dzZJv7zRFdoMOQ9EFozgpS+YbQ39"+
"MygqjaqwSpUG6Fnd8wfIXAKN4B24aBZP4aOx14jGt8XU7TzqvoyROQyD4wB3bBp+JQPdvoue6+sn"+
"/5zwC7PYlL84hgMqgIVEjCHfaeUNtk9KSBdgpoMHTj1bKjxOODVjVRkSJz+UBnER9/twCu3DxEYw"+
"DHeErwjf/pSYSAXwCVLNO1ZXSyobmTiYBQIDAQABo4IChjCCAoIwHQYDVR0OBBYEFN5sW+KMqbFw"+
"zg+E0aijwF9q6KuLMAsGA1UdDwQEAwIEsDAfBgNVHSMEGDAWgBR6e4zBz+egyhzUa/r74TPDDxqi"+
"nTCBrAYDVR0fBIGkMIGhMIGeoIGboIGYhktodHRwOi8vbXNjcmwubWljcm9zb2Z0LmNvbS9wa2kv"+
"bXNjb3JwL2NybC9NaWNyb3NvZnQlMjBJVCUyMFRMUyUyMENBJTIwNC5jcmyGSWh0dHA6Ly9jcmwu"+
"bWljcm9zb2Z0LmNvbS9wa2kvbXNjb3JwL2NybC9NaWNyb3NvZnQlMjBJVCUyMFRMUyUyMENBJTIw"+
"NC5jcmwwgYUGCCsGAQUFBwEBBHkwdzBRBggrBgEFBQcwAoZFaHR0cDovL3d3dy5taWNyb3NvZnQu"+
"Y29tL3BraS9tc2NvcnAvTWljcm9zb2Z0JTIwSVQlMjBUTFMlMjBDQSUyMDQuY3J0MCIGCCsGAQUF"+
"BzABhhZodHRwOi8vb2NzcC5tc29jc3AuY29tMD4GCSsGAQQBgjcVBwQxMC8GJysGAQQBgjcVCIfa"+
"hnWD7tkBgsmFG4G1nmGF9OtggV2E0t9CgueTegIBZAIBGzAdBgNVHSUEFjAUBggrBgEFBQcDAgYI"+
"KwYBBQUHAwEwTQYDVR0gBEYwRDBCBgkrBgEEAYI3KgEwNTAzBggrBgEFBQcCARYnaHR0cDovL3d3"+
"dy5taWNyb3NvZnQuY29tL3BraS9tc2NvcnAvY3BzMCcGCSsGAQQBgjcVCgQaMBgwCgYIKwYBBQUH"+
"AwIwCgYIKwYBBQUHAwEwJQYDVR0RBB4wHIINKi54YW1hcmluLmNvbYILeGFtYXJpbi5jb20wDQYJ"+
"KoZIhvcNAQELBQADggIBAJUeZgZrIwrG3Et02fTLRmOefgnkEhoCfeAvH5sn7Khvwt8DBEOZTYwA"+
"FFw/r4GA/0pdLRKvLXBSml29kcN8ytt6+KY43izcTtEFz6RXZe3MKNC8A42kunUj6UfvWBYHiIzK"+
"ZX1bZiB9DV0YM4Eihovm6BDF5j483T8GyijOTgNhE3hrMKq9EgEzkQXi/Y6qxjgHioIqreOVUpzi"+
"mQ1SWArcaddGpTNTORFTf1xOAt3BgbUktn3yYRaHhmUsxbpjlW+IKXY0qOejyiJZlYnQvmZO3SWW"+
"wo5ka1havUyjkrM4wEciqBXqytmPCFVT7XlfqBqN0ji2DqyIwSZfSiEENEMjQFcSU0TtpZqH5Ldm"+
"CH2kV/TiyeKuRHBZeIQJYIBkM5woUZSxdFGkL3tCUTY1bvoVCV2TykSlQdZVGbiH3fTTn72SOe9M"+
"/7pRYIpOGlJbM4XW+TCxOKnGfpdr/YbgEgp06xgKfs87/93xLTDrPZSXQomVysSdPrHaDSCSS7Cw"+
"Sb+vt1hwd1oMNLqwVH6EZAaI8qsaJz8sX/T/LzEUDBdsWg6Bb01ejRNJrjIgZsfIlFJSNwI+BUGc"+
"8hayk5wWzRpdcXXc3OvUfn+Ucqb12fnHIgQyjfpUphi/lqb0B3FuywXNNApI04C4Mo83kcFJyont"+
"I0Puxib+3v+A0M8fL1J9"}    
        };

         public void CheckClientTrusted(X509Certificate[] chain, string authType)
         {
            // Throw exception if not valid   
         }

         public void CheckServerTrusted(X509Certificate[] chain, string authType)
         {
            // Throw exception if not valid
            if (!ValidateLeaf(chain))
            {
                throw new Exception("Invalid certificate");
            }
         }

         public X509Certificate[] GetAcceptedIssuers()
         {
             return null;
         }

        private bool ValidateLeaf(X509Certificate[] chain)
        {
            if (chain.Length == 0)
            {
                return false;
            }
            return ValidateCert(chain[0]);
        }

        private bool ValidateCert(X509Certificate certificate)
        {
            var pubKeyString = Android.Util.Base64.EncodeToString(certificate.GetEncoded(), Android.Util.Base64Flags.Default).Replace("\n","");

            Console.WriteLine(pubKeyString);
            Console.WriteLine(_allowedPubKeys.Values.First());

            return _allowedPubKeys.Values.Any(a =>  a.ToLowerInvariant() == pubKeyString.ToLowerInvariant());
        }

        private bool ValidateIntermediate(X509Certificate[] chain)
        {
            if (chain.Length <= 1)
            {
                return false;
            }
            return ValidateCert(chain[1]);
        }
    }

    public class PublicKeyHandler : AndroidClientHandler
    {
        public PublicKeyHandler()
        {
            SetHandler();
        }

        private void SetHandler()
        {
            var algoritm = TrustManagerFactory.DefaultAlgorithm;
            var trustManagerFactory = TrustManagerFactory.GetInstance(algoritm);
            trustManagerFactory.Init((KeyStore)null);

            var tm = new ITrustManager[] { new PublicKeyManager() };
            SSLContext sslContext = SSLContext.GetInstance("TLS");
            sslContext.Init(null, tm, null);
            SSLContext.Default = sslContext;

            HttpsURLConnection.DefaultSSLSocketFactory = sslContext.SocketFactory;
        }
    }
}
