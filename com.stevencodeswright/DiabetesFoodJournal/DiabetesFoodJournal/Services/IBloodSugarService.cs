using DiabetesFoodJournal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public interface IBloodSugarService
    {
        Task<IEnumerable<GlucoseReading>> GetCgmReadings(DateTime logTime);
        Task<AdvancedBloodSugarStats> GetAdvancedStats();
    }
}