using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeOneFoodJournal.Entities;

namespace TypeOneFoodJournal.Business
{
    public interface IJournalEntryManager
    {
        IQueryable<JournalEntry> GetJournalEntries();
        IQueryable<JournalEntry> GetJournalEntriesForUserId(int userId);
    }
}
