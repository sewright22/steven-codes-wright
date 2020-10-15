using DiabetesFoodJournal.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public class WebService : IWebService
    {
        private HttpClient client;

        public WebService()
        {
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Add("apikey", "2367b17e3d83bcf1b93dab840aa24d62");
            this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            this.client.BaseAddress = new Uri($"{Resources.ApiBackendUrl}/");
            var email = "test2@test.com";
            var password = "test2";
            var authToken = Encoding.ASCII.GetBytes($"{email}:{password}");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
        }

        public async Task<JournalEntryDetails> GetJournalEntryDetails(int id)
        {
            var endPoint = $"journalentrydetails/v2/{id}";

            using (var response = await client.GetAsync(endPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<JournalEntryDetails>(content);
                }
            }

            return null;
        }

        public async Task<IEnumerable<JournalEntrySummary>> GetJournalEntrySummaries(int userId, string searchString)
        {
            var retVal = new List<JournalEntrySummary>();
            var endPoint = $"journalentrySummary/v2?userID={userId}&searchValue={searchString}";

            if (string.IsNullOrEmpty(searchString))
            {
                endPoint = $"journalentrySummary/v2?userID={userId}";
            }

            using (var response = await client.GetAsync(endPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var entries = JsonConvert.DeserializeObject<IEnumerable<JournalEntrySummary>>(content);
                    retVal.AddRange(entries);
                }
            }

            return retVal;
        }

        public async Task<int> CreateNewJournalEntryDetails(JournalEntryDetails journalEntryDetailsToCreate, int userId)
        {
            var serializedItem = JsonConvert.SerializeObject(journalEntryDetailsToCreate);

            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await this.client.PostAsync($"journalentrydetails/v2?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(responseContent);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var test = errorContent;
                return 0;
            }
        }

        public async Task<IEnumerable<TagModel>> GetTags(string tagSearchText)
        {
            var retVal = new List<TagModel>();//journalEntry/SearchJournal?searchValue=test
            using (var response = await client.GetAsync($"tags?searchValue={tagSearchText}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tags = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<TagModel>>(content));

                    foreach (var tag in tags)
                    {
                        retVal.Add(tag);
                    }
                }
            }

            return retVal;
        }
    }
}
