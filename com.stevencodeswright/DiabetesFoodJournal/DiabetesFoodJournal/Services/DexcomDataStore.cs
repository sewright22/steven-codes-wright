namespace DiabetesFoodJournal.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using RestSharp;
    using Xamarin.Auth;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using DiabetesFoodJournal.Models;

    public class DexcomDataStore : IDexcomDataStore
    {

        string _baseUrl = "https://sandbox-api.dexcom.com";
        string _token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiI5MzdjMjBjNy0yOThlLTQzNzgtODhiYS0zOWVhOGQ1MzIwMDgiLCJhdWQiOiJodHRwczovL3NhbmRib3gtYXBpLmRleGNvbS5jb20iLCJzY29wZSI6WyJlZ3YiLCJjYWxpYnJhdGlvbiIsImRldmljZSIsImV2ZW50Iiwic3RhdGlzdGljcyIsIm9mZmxpbmVfYWNjZXNzIl0sImlzcyI6Imh0dHBzOi8vc2FuZGJveC1hcGkuZGV4Y29tLmNvbSIsImV4cCI6MTU4ODEyNjczMiwiaWF0IjoxNTg4MTE5NTMyLCJjbGllbnRfaWQiOiJXV3YyYVBSS0xzbTlTQUVnVGNySWc4YW5SaUhLYnY1ZSJ9.bPvtDfGuYzynBo7alJdUeYUMD4_4zkg5tSR_dsWfIAzppinh096swVI8nD4jWO_pi5KRgmh8O28bmEygH93rv8cfQJaUNjlXMkcCf0b2G8MyrDhyegpGFCDgKOvRYeK9U39hoEI_GYnIX58liRve5d4USPObqBw1pKSEIlMIGo-cKQ2p4Sv2Cu5MaJMsPuFy8lLZFuVq4cElAkWKRqZ8S0EIQyM5N8CsNsgQDQ9245ADw8vRRSLeDOAeu3ymBbDqdxQqFp1qKVOKp0A9DQXepE1Dz49MMr7HLG99UQAnuMC5YIJ-qQSsTkwYVHuIAI9H1Avr9AJ5BoJdRjpwkvcPTg";
        public async Task<ReadingList> GetEGV(DateTime startTime, DateTime endTime)
        {
            // Login();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("authorization", "Bearer " + _token);
            //var url = _baseUrl + "/v2/users/self/egvs?startDate=2020-02-22T16:08:19.514&endDate=2020-02-27T12:03:33.967";
            var url = _baseUrl + $"/v2/users/self/egvs?startDate={startTime.ToString("yyyy-MM-ddTHH:mm:ss.sss")}&endDate={endTime.ToString("yyyy-MM-ddTHH:mm:ss.sss")}";
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ReadingList>(result);
                return item;
            }
        }

        private void Login()
        {
            var oauth = new OAuth2Authenticator("WWv2aPRKLsm9SAEgTcrIg8anRiHKbv5e", "x4LTtwqRWJMiaubT", "offline_access", new Uri("https://sandbox-api.dexcom.com/v2/oauth2/login"), new Uri("DiabetesFoodJournal://"), new Uri("https://sandbox-api.dexcom.com/v2/oauth2/token"));
            oauth.Completed += Oauth_Completed;
            oauth.Error += Oauth_Error;
           //AuthenticationState.Authenticator = authenticator;

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
    }

    public interface IDexcomDataStore
    {
        Task<ReadingList> GetEGV(DateTime startTime, DateTime endTime);
    }
}
