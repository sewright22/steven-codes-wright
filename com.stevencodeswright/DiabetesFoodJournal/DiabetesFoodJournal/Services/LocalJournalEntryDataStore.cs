using DiabetesFoodJournal.Data;
using DiabetesFoodJournal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DiabetesFoodJournal.Services
{
    public class LocalJournalEntryDataStore : IDataStore<JournalEntry>
    {
        private readonly IFoodJournalDatabase foodJournalDatabase;

        public LocalJournalEntryDataStore(IFoodJournalDatabase foodJournalDatabase)
        {
            this.foodJournalDatabase = foodJournalDatabase;
        }

        public async Task<bool> AddItemAsync(JournalEntry item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.InsertAsync(item).ConfigureAwait(false);
            return primaryKey > 0;
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<JournalEntry> GetItemAsync(string id)
        {
            return this.foodJournalDatabase.Database.Table<JournalEntry>().FirstOrDefaultAsync(x => x.Id.ToString().Equals(id));
        }

        public Task<List<JournalEntry>> GetItemsAsync(bool forceRefresh = false)
        {
            return this.foodJournalDatabase.Database.Table<JournalEntry>().Take(100).ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(JournalEntry item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.UpdateAsync(item).ConfigureAwait(false);
            return primaryKey > 0;
        }
    }
}
