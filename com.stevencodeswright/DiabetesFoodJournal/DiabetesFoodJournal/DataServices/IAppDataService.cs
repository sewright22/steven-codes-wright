using DiabetesFoodJournal.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public interface IAppDataService
    {
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString);
    }
}