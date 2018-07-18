using System;
using Xamarin.Forms;
using CertificatePinning.Services;
using CertificatePinning.Views;
using System.Threading.Tasks;

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

    public class SafeImage : Image
    {
        private readonly SafeService _safeService;
        public SafeImage(SafeService safeService)
        {
            _safeService = safeService;
        }

        public async Task Load(string url)
        {
            var stream = await _safeService.GetStream(url);
            Source = ImageSource.FromStream(() => stream);
        }
    }


    public class ImagePage : ContentPage
    {
        private SafeImage _image;

        public ImagePage(SafeService safeService)
        {
            _image = new SafeImage(safeService) { IsVisible = false };

            Content = _image;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _image.Load("https://d6rp199oz9kcs.cloudfront.net/dist/images/pages/index/platform-screenshot@2x-mmjfsTTi.png");
            _image.IsVisible = true;
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
            await Navigation.PushAsync(new ImagePage(_service));
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
