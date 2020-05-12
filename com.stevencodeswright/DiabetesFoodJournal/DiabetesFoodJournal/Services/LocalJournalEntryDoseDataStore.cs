using DiabetesFoodJournal.Data;
using DiabetesFoodJournal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class LocalJournalEntryDoseDataStore : IDataStore<JournalEntryDose>
    {
        private readonly IFoodJournalDatabase foodJournalDatabase;

        public LocalJournalEntryDoseDataStore(IFoodJournalDatabase foodJournalDatabase)
        {
            this.foodJournalDatabase = foodJournalDatabase;
        }
        public async Task<int> AddItemAsync(JournalEntryDose item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.InsertAsync(item).ConfigureAwait(false);
            return item.Id;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var rowsDeleted = await this.foodJournalDatabase.Database.Table<JournalEntryDose>().DeleteAsync(x => x.Id.ToString().Equals(id)).ConfigureAwait(false);
            return rowsDeleted > 0;
        }

        public Task<JournalEntryDose> GetItemAsync(string id)
        {
            return this.foodJournalDatabase.Database.Table<JournalEntryDose>().FirstOrDefaultAsync(x => x.Id.ToString().Equals(id));
        }

        public Task<List<JournalEntryDose>> GetItemsAsync(bool forceRefresh = false)
        {
            return this.foodJournalDatabase.Database.Table<JournalEntryDose>().Take(100).ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(JournalEntryDose item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.UpdateAsync(item).ConfigureAwait(false);
            return primaryKey > 0;
        }
    }
}
