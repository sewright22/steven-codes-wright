using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeOneFoodJournal.Data;
using TypeOneFoodJournal.Entities;

namespace TypeOneFoodJournal.Services.DataServices
{
    public class FoodJournalDataService : IFoodJournalDataService
    {
        private readonly FoodJournalContext context;

        public FoodJournalDataService(FoodJournalContext context)
        {
            this.context = context;
        }

        public Task<List<JournalEntry>> SearchByName(int userId, string upperSearchValue, int take, int skip = 0)
        {
            var results = this.context.UserJournalEntries.Where(u => u.UserId == userId)
                                                         .Select(uje => uje.JournalEntry)
                                                         .OrderByDescending(j => j.Logged).AsQueryable();

            if (!string.IsNullOrEmpty(upperSearchValue))
            {
                results = results.Where(entry => entry.Title.ToUpper().Contains(upperSearchValue) ||
                                                 entry.JournalEntryTags.Where(t => t.Tag.Description.ToUpper().Contains(upperSearchValue)).Any());
            }

            return results.Take(take).ToListAsync();
        }

        public Task<List<JournalEntry>> SearchByTimeFrame(int userId, DateTime startTime, DateTime endTime, int idToExclude, int take, int skip = 0)
        {
            var results = this.context.UserJournalEntries.Where(u => u.UserId == userId)
                                                         .Select(uje => uje.JournalEntry)
                                                         .Where(je=>je.Logged >= startTime && je.Logged < endTime && je.Id != idToExclude)
                                                         .OrderByDescending(j => j.Logged).AsQueryable();

            return results.Take(take).ToListAsync();
        }
    }
}
