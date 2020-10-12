using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public class JournalEntryDetailsService : IJournalEntryDetailsService
    {
        private readonly IWebService webService;

        public JournalEntryDetailsService(IWebService webService)
        {
            this.webService = webService;
        }

        public Task<JournalEntryDetails> GetDetails(int id)
        {
            return this.webService.GetJournalEntryDetails(id);
        }
    }
}
