using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<JournalEntry> CreateNewJournalEntry()
        {
            return Task.FromResult(new JournalEntry());
        }

        public IQueryable<JournalEntry> GetJournalEntries()
        {
            return this.foodJournalContext.JournalEntries;
        }

        public IQueryable<JournalEntry> GetJournalEntriesForUserId(int userId)
        {
            return from uje in this.foodJournalContext.UserJournalEntries
                   where uje.UserId == userId
                   select uje.JournalEntry;
        }

        public Task<int> Save(JournalEntryDose journalEntryDose, JournalEntryNutritionalInfo journalEntryNutritionalInfo, IEnumerable<JournalEntryTag> journalEntryTags)
        {
            this.foodJournalContext.Add(journalEntryDose);
            this.foodJournalContext.Add(journalEntryNutritionalInfo);

            if(journalEntryTags != null)
            {
                foreach (var journalEntryTag in journalEntryTags)
                {
                    this.foodJournalContext.Add(journalEntryTag);
                }
            }

            return this.foodJournalContext.SaveChangesAsync();
        }
    }
}
