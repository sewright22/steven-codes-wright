using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public interface IJournalEntryDetailsService
    {
        Task<JournalEntryDetails> GetDetails(int id);
        Task<int> Save(JournalEntryDetails journalEntryDetailsToCreate);
    }
}