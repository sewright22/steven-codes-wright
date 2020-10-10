using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using DiabetesFoodJournal.ViewModels.Journal;
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
        private readonly IJournalEntrySummaryService journalEntrySummaryService;

        public JournalDataService(IAppDataService appDataService, IUserInfo userInfo, IJournalEntrySummaryService journalEntrySummaryService)
        {
            this.appDataService = appDataService;
            this.userInfo = userInfo;
            this.journalEntrySummaryService = journalEntrySummaryService;
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

        public async Task<IEnumerable<JournalEntrySummaryViewModel>> SearchJournal(string searchString, bool isFalse)
        {
            var retVal = new List<JournalEntrySummaryViewModel>();

            this.journalEntrySummaryService.UserID = await this.userInfo.GetUserId().ConfigureAwait(false);
            this.journalEntrySummaryService.SearchString = searchString;

            var results = await this.journalEntrySummaryService.Search().ConfigureAwait(false);

            foreach (var result in results)
            {
                var viewModel = new JournalEntrySummaryViewModel()
                {
                    Model = result,
                };

                retVal.Add(viewModel);
            }

            return retVal;
        }
    }

    public interface IJournalDataService
    {
        Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave);
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString);
        Task<IEnumerable<JournalEntrySummaryViewModel>> SearchJournal(string searchString, bool isFalse);
        JournalEntryDataModel Copy(JournalEntryDataModel selectedEntry);
        JournalEntryDataModel CreateEntry(string entryTitle);
        bool CheckLogin();
    }
}
