using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class JournalDataService : IJournalDataService
    {
        private readonly IAppDataService appDataService;
        private readonly IUserInfo userInfo;

        public JournalDataService(IAppDataService appDataService, IUserInfo userInfo)
        {
            this.appDataService = appDataService;
            this.userInfo = userInfo;
        }

        public bool CheckLogin()
        {
            return false;
        }

        public JournalEntryDataModel Copy(JournalEntryDataModel selectedEntry)
        {
            var retVal = new JournalEntryDataModel();
            retVal.Load(selectedEntry.Copy());
            retVal.Dose.Load(selectedEntry.Dose.Copy());
            retVal.NutritionalInfo.Load(selectedEntry.NutritionalInfo.Copy());
            retVal.Tags.AddRange(selectedEntry.Tags.ToList());
            return retVal;
        }

        public JournalEntryDataModel CreateEntry(string entryTitle)
        {
            var entry = new JournalEntry
            {
                Title = entryTitle,
                Logged = DateTime.Now
            };

            var entryModel = new JournalEntryDataModel();
            entryModel.Load(entry);

            return entryModel;
        }

        public async Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave)
        {
            return await this.appDataService.SaveEntry(entryToSave, await this.userInfo.GetUserId().ConfigureAwait(false));
        }

        public async Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString)
        {
            return await this.appDataService.SearchJournal(await this.userInfo.GetUserId().ConfigureAwait(false), searchString.Trim());

        }
    }

    public interface IJournalDataService
    {
        Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave);
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString);
        JournalEntryDataModel Copy(JournalEntryDataModel selectedEntry);
        JournalEntryDataModel CreateEntry(string entryTitle);
        bool CheckLogin();
    }
}
