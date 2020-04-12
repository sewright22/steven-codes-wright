using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class JournalDataService : IJournalDataService
    {
        private readonly IAppDataService appDataService;

        public JournalDataService(IAppDataService appDataService)
        {
            this.appDataService = appDataService;
        }

        public async Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString)
        {
            return await this.appDataService.SearchJournal(searchString.Trim());
            
        }
    }

    public interface IJournalDataService
    {
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString);
    }
}
