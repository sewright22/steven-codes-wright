using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiabetesFoodJournal.Services;
using DiabetesFoodJournal.Views;
using XamarinHelper.Core;
using Xamarin.Auth;

namespace DiabetesFoodJournal
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ3NDgwQDMxMzgyZTMxMmUzMGQzTlEwYXdPZ3FVeGpVNVNMclZkZW9SMzZTdTdSK1JrZjN1K2R3bG9zR1U9");

            InitializeComponent();

            MainPage = new AppShell();
        }

        private void Login()
        {
            var oauth = new OAuth2Authenticator("WWv2aPRKLsm9SAEgTcrIg8anRiHKbv5e", "x4LTtwqRWJMiaubT", "offline_access", new Uri("https://sandbox-api.dexcom.com/v2/oauth2/login"), new Uri("DiabetesFoodJournal://"), new Uri("https://sandbox-api.dexcom.com/v2/oauth2/token"));
            oauth.Completed += Oauth_Completed;
            oauth.Error += Oauth_Error;

            AuthenticationState.Authenticator = oauth;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(oauth);
        }

        private void Oauth_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            var test = 5;
            var test2 = test;
        }

        private void Oauth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            var test = 5;
            var test3 = test;
        }

        protected override void OnStart()
        {
            //Login();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
