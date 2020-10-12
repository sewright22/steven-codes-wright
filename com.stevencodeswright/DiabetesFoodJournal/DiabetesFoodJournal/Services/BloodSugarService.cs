using DiabetesFoodJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public class BloodSugarService : IBloodSugarService
    {
        private readonly IDexcomDataStore dexcomDataStore;

        public BloodSugarService(IDexcomDataStore dexcomDataStore)
        {
            this.dexcomDataStore = dexcomDataStore ?? throw new ArgumentNullException(nameof(dexcomDataStore));
            this.CurrentReadings = new List<GlucoseReading>();
        }

        public List<GlucoseReading> CurrentReadings { get; }

        public async Task<IEnumerable<GlucoseReading>> GetCgmReadings(DateTime logTime)
        {
            this.CurrentReadings.Clear();
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

            this.CurrentReadings.AddRange(retVal);
            return retVal;
        }

        public async Task<AdvancedBloodSugarStats> GetAdvancedStats()
        {
            var startingReading = await Task.Run(() => this.CurrentReadings.OrderBy(x=>x.DisplayTime).FirstOrDefault(x => x.DisplayTime > 0));
            var highReading = await Task.Run(() => this.CurrentReadings.Where(x=>x.DisplayTime>=0).Aggregate((r1, r2) => r1.Reading >= r2.Reading ? r1 : r2));
            var lowReading = await Task.Run(() => this.CurrentReadings.Where(x => x.DisplayTime >= 0).Aggregate((r1, r2) => r1.Reading <= r2.Reading ? r1 : r2));

            return new AdvancedBloodSugarStats
            {
                StartingBloodSugar = startingReading.Reading,
                HighestReading = highReading.Reading,
                HighestReadingTime = highReading.DisplayTime,
                LowestReading = lowReading.Reading,
                LowestReadingTime = lowReading.DisplayTime,
            };
        }
    }
}
