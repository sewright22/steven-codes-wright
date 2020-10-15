using System.Collections.Generic;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public interface IWebService
    {
        Task<IEnumerable<JournalEntrySummary>> GetJournalEntrySummaries(int userId, string searchString);
        Task<JournalEntryDetails> GetJournalEntryDetails(int id);
        Task<int> CreateNewJournalEntryDetails(JournalEntryDetails journalEntryDetailsToCreate, int userId);
        Task<IEnumerable<TagModel>> GetTags(string tagSearchText, int? journalEntryId = null);
    }
}