using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class JournalEntryDataService  : IJournalEntryDataService
    {
        private IAppDataService appDataService;
        private readonly IUserInfo userInfo;

        public JournalEntryDataService(IAppDataService appDataService, IUserInfo userInfo)
        {
            this.appDataService = appDataService;
            this.userInfo = userInfo;
        }

        public Task<int> AddNewTag(Tag tag)
        {
            return this.appDataService.AddNewTag(tag);
        }

        public Task<IEnumerable<Tag>> GetTags(string tagSearchText)
        {
            return this.appDataService.GetTags(tagSearchText);
        }

        public async Task SaveEntry(JournalEntryDataModel model)
        {
            await this.appDataService.SaveEntry(model, await this.userInfo.GetUserId().ConfigureAwait(false)).ConfigureAwait(false);
        }
    }

    public interface IJournalEntryDataService
    {
        Task SaveEntry(JournalEntryDataModel model);
        Task<IEnumerable<Tag>> GetTags(string tagSearchText);
        Task<int> AddNewTag(Tag tag);
    }
}
