using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class BgReadingsDataService : IBgReadingsDataService
    {
        private readonly IDexcomDataStore dexcomDataStore;

        public BgReadingsDataService(IDexcomDataStore dexcomDataStore)
        {
            this.dexcomDataStore = dexcomDataStore;
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
                    DisplayTime = Convert.ToInt32(Math.Round(reading.DisplayTime.DateTime.Subtract(startTime).TotalMinutes, 0))
                });
            }

            return retVal;
        }
    }

    public interface IBgReadingsDataService
    {
        Task<IEnumerable<GlucoseReading>> GetCgmReadings(DateTime logTime);
    }
}
