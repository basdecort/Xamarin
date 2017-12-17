using Xamarin.Forms;

namespace SkiaSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SkiaSamplePage();
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
