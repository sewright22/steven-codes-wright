using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Core.Requests;
using Core.Responses;
using Services;

namespace FoodJournal.UI.Data
{
    public class DiabetesDataService : IDiabetesDataService
    {
        public DiabetesDataService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        public Task<string> GetFitbitLink(string token)
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return HttpClient.GetStringAsync("api/fitbit/link");
            }
            catch (Exception ex)
            {
                var test = ex.ToString();
                return Task.FromResult("google.com");
            }
        }

        public async Task<FoodLogResponse> GetFoodLog(string token, DateTime date)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var retVal = await  HttpClient.GetFromJsonAsync<FoodLogResponse>($"api/foodlog?Date={date.ToString("yyyy-MM-dd")}");
            var test = retVal;
            return retVal;
        }

        public Task<LoginResponse> Login(string username, string password)
        {
            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            return HttpClient.PostAsJsonAsync<LoginRequest>("api/login", loginRequest)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadFromJsonAsync<LoginResponse>();
                    }
                    else
                    {
                        return null;
                        throw new Exception(response.ReasonPhrase);
                    }
                }).Unwrap();
        }


    }
}
