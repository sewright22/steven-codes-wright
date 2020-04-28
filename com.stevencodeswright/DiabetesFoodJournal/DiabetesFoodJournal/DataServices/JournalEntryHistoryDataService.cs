using DiabetesFoodJournal.DataModels;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;

namespace DiabetesFoodJournal.DataServices
{
    public class JournalEntryHistoryDataService : IJournalEntryHistoryDataService
    {
        private readonly IAppDataService appDataService;
        private readonly IDataStore<GlucoseReading> glucoseDataStore;
        private readonly IDexcomDataStore dexcomDataStore;

        public JournalEntryHistoryDataService(IAppDataService appDataService, IDataStore<GlucoseReading> glucoseDataStore, IDexcomDataStore dexcomDataStore)
        {
            this.appDataService = appDataService;
            this.glucoseDataStore = glucoseDataStore;
            this.dexcomDataStore = dexcomDataStore;
        }

        public async Task<IEnumerable<GlucoseReading>> GetGlucoseReadings(DateTime startTime, DateTime endTime)
        {
            await this.dexcomDataStore.GetEGV();
            return await glucoseDataStore.GetItemsAsync();
        }

        public async Task<IEnumerable<Grouping<string, JournalEntryDataModel>>> SearchJournal(string searchTerm)
        {
            var entryList = await this.appDataService.SearchJournal(searchTerm);

            var sorted = from entry in entryList
                         orderby entry.Logged descending
                         group entry by entry.Group into entryGroup
                         select new Grouping<string, JournalEntryDataModel>(entryGroup.Key, entryGroup);

            return sorted;
        }


    }

    public interface IJournalEntryHistoryDataService
    {
        Task<IEnumerable<Grouping<string, JournalEntryDataModel>>> SearchJournal(string searchTerm);

        Task<IEnumerable<GlucoseReading>> GetGlucoseReadings(DateTime startTime, DateTime endTime);
    }
}
