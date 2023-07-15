using DataLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EfCore
{
    public class JournalService : IJournalService
    {
        public JournalService(sewright22_foodjournalContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public sewright22_foodjournalContext DbContext { get; }

        public Task<List<Journalentry>> SearchEntries(string searchValue)
        {
            return this.DbContext.Journalentries
                .Include(x => x.JournalEntryNutritionalInfo)
                .ThenInclude(x => x == null ? null : x.Nutritionalinfo)
                .Where(x => x == null ? false :
                x.Title == null ? false :
                x.Title.ToLower().Contains(searchValue.ToLower()) ||
                     x.JournalEntryTags == null ? false :
                     x.JournalEntryTags.Any(x => x.Tag == null ? false :
                     x.Tag.Description == null ? false :
                     x.Tag.Description.ToLower().Contains(searchValue.ToLower()))).ToListAsync();
        }
    }
}
