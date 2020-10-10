using System;
using System.Linq;
using TypeOneFoodJournal.Data;
using TypeOneFoodJournal.Entities;

namespace TypeOneFoodJournal.Business.EFCore
{
    public class JournalEntryManager : IJournalEntryManager
    {
        private readonly FoodJournalContext foodJournalContext;

        public JournalEntryManager(FoodJournalContext foodJournalContext)
        {
            this.foodJournalContext = foodJournalContext;
        }

        public IQueryable<JournalEntry> GetJournalEntriesForUserId(int userId)
        {
            return from uje in this.foodJournalContext.UserJournalEntries
                   where uje.UserId == userId
                   select uje.JournalEntry;
        }
    }
}
