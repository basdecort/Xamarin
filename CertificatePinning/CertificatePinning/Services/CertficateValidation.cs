using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

namespace CertificatePinning.Services
{
    public static class CertficateValidation
    {
        public static void Init()
        {
            ServicePointManager.ServerCertificateValidationCallback += ValidatePubKey; 
        }

        public static Dictionary<string,string> AllowedPublicKeys = new Dictionary<string, string>()
        {
            {"Cloudfront.com","3082010A0282010100A3D28C67E1CABBD73FCACB44F10048AC379AF5970F92E1BAFE08B43B49A9DAB46C6021DABCCFD2C8147285D20DE40D32F6D5FC977122D0CE6C4E61744C25D2F0999CB7087ED699A8907A2D10953E52D352F62A96DE7B017ECBA869AE1AE9485D6F80A2C36DA38C9ABC097F88DFFDB61EEF5D1BF35D8B61F2171D82BABCCB321B41C45D86C4D01BFE99D33E9AEC6160232487C3054F9BBBFC6120D4DE3A6DA8D054C8E9C217BAF8315AA6BB38C388709C19D83880C62C3D7F84E4C388BEBD2EF0BADEC9451A5F67355FDB733B2ACD24F9B23470D274145B089A25AB74427514769C85029D33807F4C53D5BF394ABE3E8DDC21ACD557CFCA9F1EA56DC061670F790203010001"},
            {"Microsoft", "3082010A0282010100CCEAE2843C1BA9352E015D159D854E91CDAC153F6EE5168E1E8803A5A041DA5D83350E83D4271C6DFAECA1C2493CC8864528B2BD00A5F4AADA935453A1DD3164EFBB8624A95FCAE82956CFB9B0619F7E1774CB67064B23A5B492DC7FFBF7D6D46378DFF1362F42787B5C2B8EA4B2A829F647530DDD48BB10CEF5F378E4B44F66446E3A9372C9700794CC950CEE177E0B7C0981FFB2C9ABD59A98AFDF1D3BD880894F9E16BCFA86E0420097C5CCC5D6CE76E9C2BB1DE354E3139CCF2641DA07D04E2AE2D9C927C44212117B07B116D257953F3C2A3E927C8A1EDA7699C6A0D6FED415573471203D3DDA65DD5448CBD8C67A1A870D935A4F7B3A98F303948E003B0203010001"},
            {"Microsoft1", "3082010A0282010100C35CD7F271D965FF6B0A5BE78699820C3B80E45A2CC38FFBF635F7681DCC8F632F6ED487C5E07686FD114DDB81E4EFACCDC0CEFF6F047EE2B70D5FB6A96506FDF9A3A35D08C6A9057C8788EB4D319A4A74BBE898BFCD3BD1B86828C1CE4D5C30D1F3F9467E610570DEA39B4DA57628D51034D8D1164038F4908FE488EF6C59A23A8E6FBC3E6413B132E46EF4C6D5793F865F86C00FBD70ED2ABC05AD5D2DC074C5DC6CDBF160114F498EEFD95D63FDDE151F5FFA1A2E3F36D7FAE0FC63DAC6A147FA7B517A8A2E5DBDC837C4085FB01E7AB99B40813EAFFCD16BEFFAB7CF12CC274450D52C9ADCB02E66B6A98FEB394D11D4F5B54F7AD93DA23E66BDCB6CC9690203010001"}
        };

        /// <summary>
        /// Validates the certificate.
        /// </summary>
        /// <returns><c>true</c>, if certificate was validated, <c>false</c> otherwise.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="certificate">Certificate.</param>
        /// <param name="chain">Certificate chain sent by server.</param>
        /// <param name="sslPolicyErrors">Errors like name mismatch.</param>
        private static bool ValidatePubKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var publicKey = certificate?.GetPublicKeyString();
            var isValid = AllowedPublicKeys.Values.Contains(publicKey);
            return isValid;
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

            return AllowedPublicKeys.Values.Contains(intermediateCertificate?.GetPublicKeyString());
        }


        /// <summary>
        /// Gets the pub key from cert file
        /// </summary>
        /// <returns>The pub key.</returns>
        /// <param name="certFile">Cert file.</param>
        public static string GetPubKey(string certFile)
        {
            var cert = X509Certificate.CreateFromCertFile(certFile);
            return cert.GetPublicKeyString();
        }
    }
}
