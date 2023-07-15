using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Responses;
using Newtonsoft.Json;

namespace Services.External
{
    public class FitbitService : IFitbitService
    {
        public FitbitService(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        private string TokenUrl => "https://api.fitbit.com/oauth2/token";

        private string AuthorizeUrl => "https://www.fitbit.com/oauth2/authorize";

        public HttpClient HttpClient { get; }

        public string BuildAuthorizationUrl(string clientId, string redirectUrl, string scope, string state)
        {
            return $"{this.AuthorizeUrl}?response_type=code&client_id={clientId}&redirect_uri={redirectUrl}&scope={scope}&state={state}";
        }

        public async Task<IResponseToken> GetAccessToken(string? clientId, string? clientSecret, string? code, string? redirectUrl)
        {
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}")));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUrl)
            });
            var response = await this.HttpClient.PostAsync(this.TokenUrl, content);
            var json = await response.Content.ReadAsStringAsync();
            var fitBitToken = Newtonsoft.Json.JsonConvert.DeserializeObject<FitbitToken>(json);
            return fitBitToken;
        }

        public async Task<string> GetClientId(string token, string userId)
        {
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await this.HttpClient.GetAsync($"https://api.fitbit.com/1/user/{userId}/profile.json");
            var json = await response.Content.ReadAsStringAsync();
            var fitBitProfile = Newtonsoft.Json.JsonConvert.DeserializeObject<FitbitProfile>(json);
            return fitBitProfile.User.EncodedId;
        }

        public async Task<FoodLogResponse> GetFoodLog(string token, string userId, DateTime dateTime)
        {
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var foodLogUrl = $"https://api.fitbit.com/1/user/{userId}/foods/log/date/{dateTime.ToString("yyyy-MM-dd")}.json";
            var response = await this.HttpClient.GetAsync(foodLogUrl);
            var json = await response.Content.ReadAsStringAsync();
            var foodLog = Newtonsoft.Json.JsonConvert.DeserializeObject<FoodLogResponse>(json);
            return foodLog;
        }
    }

    public class FitbitToken : IResponseToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
