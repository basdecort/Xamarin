using System;
using Xamarin.Forms;
using CertificatePinning.Services;
using CertificatePinning.Views;

namespace CertificatePinning
{
    public class CustomWebView : WebView
    {
         
    }

    public class WebPage : ContentPage
    {
        public WebPage()
        {
            /* easy approach
             * var browser = new CustomCrossWebView();
            browser.Open("https://microsoft.com");
            */

            // With custom renderer
            Content = new CustomWebView() { Source= "https://microsoft.com"};
        }
    }

    public class ImagePage : ContentPage
    {
        public ImagePage()
        {
            var image = new Image();
            image.Source = "https://d6rp199oz9kcs.cloudfront.net/dist/images/pages/index/platform-screenshot@2x-mmjfsTTi.png";
            Content = image;
        }
    }

    public partial class MainPage : ContentPage
    {
        private SafeService _service;
        public MainPage()
        {
            InitializeComponent();
            _service = new SafeService();
        }

        async void Handle_WebView(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new WebPage());
        }

        async void Handle_ImageView(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ImagePage());
        }

        async void Handle_HttpClient(object sender, System.EventArgs e)
        {
            base.OnAppearing();
            try
            {
                var result = await _service.GetContents("https://www.microsoft.com/");
                await DisplayAlert("Success", result, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error occurred: {ex.Message}", "OK");
            }
        }
    }
}
