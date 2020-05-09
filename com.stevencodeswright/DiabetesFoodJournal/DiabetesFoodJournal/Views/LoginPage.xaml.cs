using DiabetesFoodJournal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace DiabetesFoodJournal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Login2();
        }

        private string client_id = "WWv2aPRKLsm9SAEgTcrIg8anRiHKbv5e";
        private string client_secret = "x4LTtwqRWJMiaubT";
        private string scope = "offline_access";
        //private string redirectString = "msauth://com.stevencodeswright.diabetesfoodjournal/pykA5WfgEWUtSw49a2kDFXvW5uo%3D";
        private string redirectString = "com.stevencodeswright.diabetesfoodjournal://";
        private string callback = "com.stevencodeswright.diabetesfoodjournal://";

        private async void Login2()
        {
            var oauth = await Xamarin.Essentials.WebAuthenticator.AuthenticateAsync(
                                                                new Uri($"https://sandbox-api.dexcom.com/v2/oauth2/login?client_id={client_id}&redirect_uri={redirectString}&response_type=code&scope={scope}"),
                                                                new Uri(callback));

            var accessToken = oauth?.Get("code");
            var test = accessToken;

            //var client = new RestClient("https://api.dexcom.com/v2/oauth2/token");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("application/x-www-form-urlencoded", "client_secret={your_client_secret}&client_id={your_client_id}&code={your_authorization_code}&grant_type=authorization_code&redirect_uri={your_redirect_uri}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
        }
    }
}