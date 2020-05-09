using DiabetesFoodJournal.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class JournalEntryDataService  : IJournalEntryDataService
    {
        private IAppDataService appDataService;

        public JournalEntryDataService(IAppDataService appDataService)
        {
            this.appDataService = appDataService;
        }

        public Task SaveEntry(JournalEntryDataModel model)
        {
            return this.appDataService.SaveEntry(model);
        }
    }

    public interface IJournalEntryDataService
    {
        Task SaveEntry(JournalEntryDataModel model);
    }
}
