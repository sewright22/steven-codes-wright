using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public class JournalEntrySummaryService : IJournalEntrySummaryService
    {
        private readonly IWebService webService;
        private readonly List<JournalEntrySummary> localList;

        public JournalEntrySummaryService(IWebService webService)
        {
            this.webService = webService ?? throw new ArgumentNullException(nameof(webService));
            this.localList = new List<JournalEntrySummary>();
        }

        public int UserID { get; set; }
        public string SearchString { get; set; }

        public async Task<IEnumerable<JournalEntrySummary>> Search()
        {
            this.localList.Clear();

            this.localList.AddRange(await this.webService.GetJournalEntrySummaries(this.UserID, this.SearchString).ConfigureAwait(false));

            return this.localList;
        }
    }
}
