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
        private readonly IUserInfo userInfo;
        private string baseUrl = "https://api.dexcom.com";
        private string client_id = "WWv2aPRKLsm9SAEgTcrIg8anRiHKbv5e";
        private string client_secret = "x4LTtwqRWJMiaubT";
        private string redirectString = "com.stevencodeswright.diabetesfoodjournal://";
        private string callback = "com.stevencodeswright.diabetesfoodjournal://";
        private string scope = "offline_access";

        public DexcomDataStore(IUserInfo userInfo)
        {
            this.userInfo = userInfo;
        }

        public async Task<ReadingList> GetEGV(DateTime startTime, DateTime endTime)
        {
            var token = await this.userInfo?.GetDexcomToken();
            var refreshToken = await this.userInfo?.GetDexcomRefreshToken();


            if (string.IsNullOrEmpty(token) && string.IsNullOrEmpty(refreshToken))
            {
                await this.Login();
            }
            else 
            {
                await this.RefreshToken(refreshToken);
            }

            token = await this.userInfo?.GetDexcomToken();
            refreshToken = await this.userInfo?.GetDexcomRefreshToken();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("authorization", "Bearer " + token);
            var url = baseUrl + $"/v2/users/self/egvs?startDate={startTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss")}&endDate={endTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss")}";
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ReadingList>(result);
                return item;
            }
        }

        private async Task RefreshToken(string refreshToken)
        {
            var client = new HttpClient();
            var parameters = new Dictionary<string, string>
            {
                { "client_secret", client_secret }
              , { "client_id", client_id }
              , { "refresh_token", refreshToken}
              , { "grant_type", "refresh_token"}
              , { "redirect_uri", redirectString}
            };

            var encodedContent = new FormUrlEncodedContent(parameters);

            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            using (var response = await client.PostAsync("https://api.dexcom.com/v2/oauth2/token", encodedContent))
            {
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeAnonymousType(result, new
                { access_token = "", expires_in = 0, token_type = "", refresh_token = "" });

                await this.userInfo.SetDexcomToken(item.access_token).ConfigureAwait(false);
                await this.userInfo.SetDexcomRefreshToken(item.refresh_token);
            }
        }

        public async Task Login()
        {
            var oauth = await Xamarin.Essentials.WebAuthenticator.AuthenticateAsync(
                                                                   new Uri($"https://api.dexcom.com/v2/oauth2/login?client_id={client_id}&redirect_uri={redirectString}&response_type=code&scope={scope}"),
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

            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            using (var response = await client.PostAsync("https://api.dexcom.com/v2/oauth2/token", encodedContent))
            {
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeAnonymousType(result, new
                { access_token = "", expires_in = 0, token_type = "", refresh_token = "" });

                await this.userInfo.SetDexcomToken(item.access_token).ConfigureAwait(false);
                await this.userInfo.SetDexcomRefreshToken(item.refresh_token);
            }
        }
    }

    public interface IDexcomDataStore
    {
        Task<ReadingList> GetEGV(DateTime startTime, DateTime endTime);
        Task Login();
    }
}
