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

        public JournalDataService(IAppDataService appDataService)
        {
            this.appDataService = appDataService;
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

        public async Task<int> SaveEntry(JournalEntryDataModel entryToSave)
        {
            return await this.appDataService.SaveEntry(entryToSave);
        }

        public async Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString)
        {
            return await this.appDataService.SearchJournal(searchString.Trim());

        }
    }

    public interface IJournalDataService
    {
        Task<int> SaveEntry(JournalEntryDataModel entryToSave);
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString);
        JournalEntryDataModel Copy(JournalEntryDataModel selectedEntry);
        JournalEntryDataModel CreateEntry(string entryTitle);
    }
}
