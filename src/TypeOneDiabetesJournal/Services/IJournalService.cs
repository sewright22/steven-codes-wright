using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IJournalService
    {
        /// <summary>
        ///  Method to search for journal entries. ICollection allows a collection of Journalentries.
        /// </summary>
        /// <param name="searchValue">String value to be searched partial match.</param>
        /// <returns>A list of journal entries that match the search value.</returns>
        public Task<List<Journalentry>> SearchEntries(string searchValue);
    }
}
