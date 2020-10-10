using System.Collections.Generic;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public interface IJournalEntrySummaryService
    {
        string SearchString { get; set; }
        int UserID { get; set; }

        Task<IEnumerable<JournalEntrySummary>> Search();
    }
}