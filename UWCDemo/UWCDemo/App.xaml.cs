using Xamarin.Forms;

namespace UWCDemo
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<IHttpJokes, HttpJokes>();
            DependencyService.Register<IDbJokes, DbJokes>();

            InitializeComponent();

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
