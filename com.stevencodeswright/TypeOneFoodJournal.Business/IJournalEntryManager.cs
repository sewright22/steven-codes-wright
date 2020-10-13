using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Entities;

namespace TypeOneFoodJournal.Business
{
    public interface IJournalEntryManager
    {
        IQueryable<JournalEntry> GetJournalEntries();
        IQueryable<JournalEntry> GetJournalEntriesForUserId(int userId);
        Task<JournalEntry> CreateNewJournalEntry();
        Task<int> Save(JournalEntryDose journalEntryDose, JournalEntryNutritionalInfo journalEntryNutritionalInfo, IEnumerable<JournalEntryTag> journalEntryTags);
    }
}
