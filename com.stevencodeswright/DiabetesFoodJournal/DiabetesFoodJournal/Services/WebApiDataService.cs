using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Factories;
using DiabetesFoodJournal.Properties;
using DiabetesFoodJournal.WebApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class WebApiDataService : IAppDataService
    {
        private readonly IJournalEntryFactory journalEntryFactory;
        private readonly IDoseFactory doseFactory;
        private readonly INutritionalInfoFactory nutritionalInfoFactory;
        private readonly ITagFactory tagFactory;
        private HttpClient client;

        public WebApiDataService(IJournalEntryFactory journalEntryFactory, IDoseFactory doseFactory, INutritionalInfoFactory nutritionalInfoFactory, ITagFactory tagFactory)
        {
            this.client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", "2367b17e3d83bcf1b93dab840aa24d62");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.BaseAddress = new Uri($"{Resources.ApiBackendUrl}/");
            this.journalEntryFactory = journalEntryFactory;
            this.doseFactory = doseFactory;
            this.nutritionalInfoFactory = nutritionalInfoFactory;
            this.tagFactory = tagFactory;
        }

        public async Task<int> AddNewTag(Tag tag)
        {
            var serializedItem = JsonConvert.SerializeObject(tag);

            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await this.client.PostAsync("tags", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var retVal = JsonConvert.DeserializeObject<Tag>(responseContent);
                return retVal.Id;
            }
            else
            {
                return -1;
            }
        }

        public async Task<IEnumerable<Tag>> GetTags(string tagSearchText)
        {
            var retVal = new List<Tag>();//journalEntry/SearchJournal?searchValue=test
            using (var response = await client.GetAsync($"tags?searchValue={tagSearchText}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tags = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Tag>>(content));

                    foreach (var tag in tags)
                    {
                        retVal.Add(tag);
                    }
                }
            }

            return retVal;
        }

        public async Task<UserDataModel> Login(string email, string password)
        {
            var retVal = new UserDataModel();
            var authToken = Encoding.ASCII.GetBytes($"{email}:{password}");
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(authToken));

            using (var response = await this.client.GetAsync($"users?email={email}").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var users = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<User>>(content)).ConfigureAwait(false);
                    var user = users.SingleOrDefault();

                    if(user != null)
                    {
                        retVal.Load(user);
                    }
                }
            }

            return retVal;
        }

        public async Task<UserDataModel> CreateAccount(string email, string password)
        {
            var user = new User();
            user.Email = email;
            user.Password = password;

            var serializedItem = JsonConvert.SerializeObject(user);

            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await this.client.PostAsync($"users/createaccount", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var retVal = JsonConvert.DeserializeObject<User>(responseContent);
                var userModel = new UserDataModel();
                user.Id = retVal.Id;
                userModel.Load(user);
                return userModel;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var test = errorContent;
                return null;
            }
        }

        public Task<int> SaveDose(DoseDataModel doseToSave)
        {
            throw new NotImplementedException();
        }

        public async Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave, int userId)
        {
            var serializedItem = JsonConvert.SerializeObject(entryToSave);

            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await this.client.PostAsync($"journalEntries/v2?userId={userId}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var retVal = JsonConvert.DeserializeObject<JournalEntryDataModel>(responseContent);
                entryToSave.Id = retVal.Id;
                entryToSave.Dose.Id = retVal.Dose.Id;
                entryToSave.NutritionalInfo.Id = retVal.NutritionalInfo.Id;
                return entryToSave;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var test = errorContent;
                return null;
            }
        }

        public Task<int> SaveNurtritionalInfo(NutritionalInfoDataModel nutritionalInfoToSave)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JournalEntryDataModel>> SearchJournal(int userId, string searchString)
        {
            var retVal = new List<JournalEntryDataModel>();//journalEntry/SearchJournal?searchValue=test
            var endPoint = $"journalEntries/V2/?userId={userId}&searchValue={searchString}";

            if(string.IsNullOrEmpty(searchString))
            {
                endPoint = $"journalEntries/V2/?userId={userId}";
            }

            using (var response = await client.GetAsync(endPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var entries = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<JournalEntryWebApiModel>>(content));

                    foreach (var entry in entries)
                    {
                        var entryDataModel = new JournalEntryDataModel();
                        entryDataModel.Load(this.journalEntryFactory.Build(entry));
                        entryDataModel.Dose.Load(this.doseFactory.Build(entry.Dose));
                        entryDataModel.NutritionalInfo.Load(this.nutritionalInfoFactory.Build(entry.NutritionalInfo));

                        foreach (var tag in entry.Tags)
                        {
                            var tagDataModel = new TagDataModel();
                            tagDataModel.Load(this.tagFactory.Build(tag));
                            entryDataModel.Tags.Add(tagDataModel);
                        }

                        retVal.Add(entryDataModel);
                    }
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //var message = await Task.Run(() => JsonConvert.DeserializeObject<string>(content));
                    //var test = message;
                }
            }

            return retVal;
        }

        public async Task<IEnumerable<JournalEntryDataModel>> SearchJournal(int userId, DateTime startTime, DateTime endTime, int idToExclude)
        {
            var retVal = new List<JournalEntryDataModel>();//journalEntry/SearchJournal?searchValue=test
            var endPoint = $"journalEntries/V2/?userId={userId}&startTime={startTime.ToString("f")}&endTime={endTime.ToString("f")}&idToExclude={idToExclude}";

            using (var response = await client.GetAsync(endPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var entries = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<JournalEntryWebApiModel>>(content));

                    foreach (var entry in entries)
                    {
                        var entryDataModel = new JournalEntryDataModel();
                        entryDataModel.Load(this.journalEntryFactory.Build(entry));
                        entryDataModel.Dose.Load(this.doseFactory.Build(entry.Dose));
                        entryDataModel.NutritionalInfo.Load(this.nutritionalInfoFactory.Build(entry.NutritionalInfo));

                        foreach (var tag in entry.Tags)
                        {
                            var tagDataModel = new TagDataModel();
                            tagDataModel.Load(this.tagFactory.Build(tag));
                            entryDataModel.Tags.Add(tagDataModel);
                        }

                        retVal.Add(entryDataModel);
                    }
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //var message = await Task.Run(() => JsonConvert.DeserializeObject<string>(content));
                    //var test = message;
                }
            }

            return retVal;
        }
    }
}
