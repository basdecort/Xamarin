using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Net;
using System.Diagnostics;

namespace ServicePointManagerCallback
{
    public partial class MainPage : ContentPage
    {
        private HttpClient _httpClient;
        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback += ServicePointManager_ServerCertificateValidationCallback;
        }

        bool ServicePointManager_ServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // This should be called
            Debugger.Break();

            // Certificate pinning can be done over here..

            return true;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await _httpClient.GetStringAsync("https://www.xamarin.com");
        }
    }
}
