using LifePlanner.Sms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LifePlanner
{
    public partial class App : Application
    {
        public App(ISmsReader smsReader)
        {
            InitializeComponent();

            MainPage = new MainPage(smsReader);
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
