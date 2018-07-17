using System;
using CertificatePinning.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CertificatePinning
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

          //  var pubkey = CertficateValidation.GetPubKey("www.microsoft.com.cer");

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
