using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CertificatePinning.Services;
using CertificatePinning.Droid.Handlers;

namespace CertificatePinning.Droid
{
    [Activity(Label = "CertificatePinning", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // Native verification using TrustManager, Change project properties to use correct implementation!
            SafeService.HttpClient = SafeService.CreateClient(new PublicKeyHandler());

            // Managed verification, Change project properties to use correct implementation!
            //SafeService.HttpClient = SafeService.CreateClient();

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

