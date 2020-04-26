using DiabetesFoodJournal.DataModels;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class JournalEntryHistoryDataService : IJournalEntryHistoryDataService
    {
        private readonly IAppDataService appDataService;

        public JournalEntryHistoryDataService(IAppDataService appDataService)
        {
            this.appDataService = appDataService;
        }
        public async Task<IEnumerable<Grouping<string, JournalEntryDataModel>>> SearchJournal(string searchTerm)
        {
            var entryList = await this.appDataService.SearchJournal(searchTerm);

            var sorted = from entry in entryList
                         orderby entry.Logged descending
                         group entry by entry.Group into entryGroup
                         select new Grouping<string, JournalEntryDataModel>(entryGroup.Key, entryGroup);

            return sorted;
        }
    }

    public interface IJournalEntryHistoryDataService
    {
        Task<IEnumerable<Grouping<string, JournalEntryDataModel>>> SearchJournal(string searchTerm);
    }
}
