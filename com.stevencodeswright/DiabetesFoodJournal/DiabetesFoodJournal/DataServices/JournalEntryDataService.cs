using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class JournalEntryDataService  : IJournalEntryDataService
    {
        private IAppDataService appDataService;

        public JournalEntryDataService(IAppDataService appDataService)
        {
            this.appDataService = appDataService;
        }

        public Task<int> AddNewTag(Tag tag)
        {
            return this.appDataService.AddNewTag(tag);
        }

        public Task<IEnumerable<Tag>> GetTags(string tagSearchText)
        {
            return this.appDataService.GetTags(tagSearchText);
        }

        public Task SaveEntry(JournalEntryDataModel model)
        {
            return this.appDataService.SaveEntry(model);
        }
    }

    public interface IJournalEntryDataService
    {
        Task SaveEntry(JournalEntryDataModel model);
        Task<IEnumerable<Tag>> GetTags(string tagSearchText);
        Task<int> AddNewTag(Tag tag);
    }
}
