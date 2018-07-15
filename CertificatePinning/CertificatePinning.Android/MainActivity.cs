
using Android.App;
using Android.Content.PM;
using Android.OS;
using CertificatePinning.Droid.Handlers;
using CertificatePinning.Services;

namespace CertificatePinning.Droid
{
    [Activity(Label = "CertificatePinning", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // Native verification using TrustManager, works with AndroidClientHandler!
            SafeService.HttpClient = SafeService.CreateClient(new PublicKeyHandler());

            // Managed verification, works with Managed client handler!
            //SafeService.HttpClient = SafeService.CreateClient();

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

