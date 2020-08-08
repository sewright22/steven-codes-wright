using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class BgReadingsDataService : IBgReadingsDataService
    {
        private readonly IDexcomDataStore dexcomDataStore;
        private readonly IAppDataService appDataService;
        private readonly IUserInfo userInfo;

        public BgReadingsDataService(IDexcomDataStore dexcomDataStore, IAppDataService appDataService, IUserInfo userInfo)
        {
            this.dexcomDataStore = dexcomDataStore;
            this.appDataService = appDataService;
            this.userInfo = userInfo;
        }

        public async Task<IEnumerable<GlucoseReading>> GetCgmReadings(DateTime logTime)
        {
            var retVal = new List<GlucoseReading>();
            var startTime = logTime.AddMinutes(-30);
            var endTime = logTime.AddHours(5);
            var readings = await this.dexcomDataStore.GetEGV(startTime, endTime);

            foreach (var reading in readings.Egvs)
            {
                retVal.Add(new GlucoseReading
                {
                    Reading = reading.RealtimeValue.HasValue ? reading.RealtimeValue.Value : (float?)null,
                    DisplayTime = Convert.ToInt32(Math.Round(reading.DisplayTime.DateTime.Subtract(logTime).TotalMinutes, 0))
                });
            }

            return retVal;
        }

        public async Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave)
        {
            return await this.appDataService.SaveEntry(entryToSave, await this.userInfo.GetUserId().ConfigureAwait(false));
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

        public async Task<IEnumerable<ChartReading>> GetOtherEntries(DateTime logTime, int idToExclude)
        {
            var retVal = new List<ChartReading>();
            var startTime = logTime.AddMinutes(-30);
            var endTime = logTime.AddHours(5);
            var userId = await this.userInfo.GetUserId();

            var otherEntries = await this.appDataService.SearchJournal(userId, startTime, endTime, idToExclude);

            //Convert to glucose readings, so they can be put on the chart.

            foreach (var entry in otherEntries)
            {
                var insulinAmountString = "";

                if(entry.Dose.Extended>0)
                {
                    insulinAmountString = $"{entry.Dose.InsulinAmount}u ({entry.Dose.UpFront}/{entry.Dose.Extended} - {entry.Dose.TimeExtended}H)";
                }
                else
                {
                    insulinAmountString = $"{entry.Dose.InsulinAmount}u";
                }

                retVal.Add(new ChartReading
                {
                    Reading = 60,
                    DisplayTime = Convert.ToInt32(Math.Round(entry.Logged.Subtract(logTime).TotalMinutes, 0)),
                    FoodName = entry.Model.Title,
                    InsulinAmount = insulinAmountString,
                });
            }

            return retVal;
        }
    }

    public interface IBgReadingsDataService
    {
        Task<IEnumerable<ChartReading>> GetOtherEntries(DateTime logTime, int idToExclude);
        Task<IEnumerable<GlucoseReading>> GetCgmReadings(DateTime logTime);
        Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave);
        JournalEntryDataModel Copy(JournalEntryDataModel selectedEntry);
    }
}
