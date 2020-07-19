using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeOneFoodJournal.Entities;

namespace TypeOneFoodJournal.Services.DataServices
{
    public interface IFoodJournalDataService
    {
        Task<List<JournalEntry>> SearchByName(int userId, string upperSearchValue, int take, int skip = 0);
        Task<List<JournalEntry>> SearchByTimeFrame(int userId, DateTime startTime, DateTime endime, int idToExclude, int take, int skip = 0);
    }
}
