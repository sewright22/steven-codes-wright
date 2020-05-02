namespace DiabetesFoodJournal.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using Xamarin.Auth;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using DiabetesFoodJournal.Models;
    using System.Collections.Generic;

    public class DexcomDataStore : IDexcomDataStore
    {
        private string token;
        private string refreshToken;
        private string baseUrl = "https://sandbox-api.dexcom.com";
        private string client_id = "WWv2aPRKLsm9SAEgTcrIg8anRiHKbv5e";
        private string client_secret = "x4LTtwqRWJMiaubT";
        private string redirectString = "com.stevencodeswright.diabetesfoodjournal://";
        private string callback = "com.stevencodeswright.diabetesfoodjournal://";
        private string scope = "offline_access";

        public async Task<ReadingList> GetEGV(DateTime startTime, DateTime endTime)
        {
            if (string.IsNullOrEmpty(token) && string.IsNullOrEmpty(refreshToken))
            {
                await this.Login();
            }

            // Login();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("authorization", "Bearer " + token);
            //var url = _baseUrl + "/v2/users/self/egvs?startDate=2020-02-22T16:08:19.514&endDate=2020-02-27T12:03:33.967";
            var url = baseUrl + $"/v2/users/self/egvs?startDate={startTime.ToString("yyyy-MM-ddTHH:mm:ss")}&endDate={endTime.ToString("yyyy-MM-ddTHH:mm:ss")}";
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ReadingList>(result);
                return item;
            }
        }

        public async Task Login()
        {
            var oauth = await Xamarin.Essentials.WebAuthenticator.AuthenticateAsync(
                                                                   new Uri($"https://sandbox-api.dexcom.com/v2/oauth2/login?client_id={client_id}&redirect_uri={redirectString}&response_type=code&scope={scope}"),
                                                                   new Uri(callback));

            var accessToken = oauth?.Get("code");

            var client = new HttpClient();
            var parameters = new Dictionary<string, string>
            {
                { "client_secret", client_secret }
              , { "client_id", client_id }
              , { "code", accessToken}
              , { "grant_type", "authorization_code"}
              , { "redirect_uri", redirectString}
            };

            var encodedContent = new FormUrlEncodedContent(parameters);

            //encodedContent.Headers.Add("content-type", "application/x-www-form-urlencoded");
            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            //request.AddParameter("application/x-www-form-urlencoded", "client_secret={your_client_secret}&client_id={your_client_id}&code={your_authorization_code}&grant_type=authorization_code&redirect_uri={your_redirect_uri}", ParameterType.RequestBody);
            using (var response = await client.PostAsync("https://sandbox-api.dexcom.com/v2/oauth2/token", encodedContent))
            {
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeAnonymousType(result, new
                { access_token = "", expires_in = 0, token_type = "", refresh_token = "" });

                token = item.access_token;
                refreshToken = item.refresh_token;
            }
        }
    }

    public interface IDexcomDataStore
    {
        Task<ReadingList> GetEGV(DateTime startTime, DateTime endTime);
        Task Login();
    }
}
