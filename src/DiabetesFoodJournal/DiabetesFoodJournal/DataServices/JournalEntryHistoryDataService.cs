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
        private readonly IDexcomDataStore dexcomDataStore;
        private readonly IUserInfo userInfo;

        public JournalEntryHistoryDataService(IAppDataService appDataService, IDexcomDataStore dexcomDataStore, IUserInfo userInfo)
        {
            this.appDataService = appDataService;
            this.dexcomDataStore = dexcomDataStore;
            this.userInfo = userInfo;
        }

        public async Task<IEnumerable<GlucoseReading>> GetGlucoseReadings(DateTime startTime, DateTime endTime)
        {
            var retVal = new List<GlucoseReading>();
            var readings = await this.dexcomDataStore.GetEGV(startTime, endTime);

            foreach(var reading in readings.Egvs)
            {
                retVal.Add(new GlucoseReading
                {
                    Reading = reading.RealtimeValue.HasValue ? reading.RealtimeValue.Value : (float?)null,
                    DisplayTime = Convert.ToInt32(Math.Round(reading.DisplayTime.DateTime.Subtract(startTime).TotalMinutes, 0))
                });
            }

            return retVal;
        }

        public async Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchTerm)
        {
            var entryList = await this.appDataService.SearchJournal(await this.userInfo.GetUserId().ConfigureAwait(false), searchTerm);

            //var sorted = from entry in entryList
            //             orderby entry.Logged descending
            //             group entry by entry.Group into entryGroup
            //             select new Grouping<string, JournalEntryDataModel>(entryGroup.Key, entryGroup);

            return entryList;
        }


    }

    public interface IJournalEntryHistoryDataService
    {
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchTerm);

        Task<IEnumerable<GlucoseReading>> GetGlucoseReadings(DateTime startTime, DateTime endTime);
    }
}
