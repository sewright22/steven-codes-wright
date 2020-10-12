﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public interface IWebService
    {
        Task<IEnumerable<JournalEntrySummary>> GetJournalEntrySummaries(int userId, string searchString);
        Task<JournalEntryDetails> GetJournalEntryDetails(int id);
    }
}