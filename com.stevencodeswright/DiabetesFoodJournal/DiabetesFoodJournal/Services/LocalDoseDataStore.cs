using DiabetesFoodJournal.Data;
using DiabetesFoodJournal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class LocalDoseDataStore : IDataStore<Dose>
    {
        private readonly IFoodJournalDatabase foodJournalDatabase;

        public LocalDoseDataStore(IFoodJournalDatabase foodJournalDatabase)
        {
            this.foodJournalDatabase = foodJournalDatabase;
        }
        public async Task<int> AddItemAsync(Dose item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.InsertAsync(item).ConfigureAwait(false);
            return primaryKey;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var rowsDeleted = await this.foodJournalDatabase.Database.Table<Dose>().DeleteAsync(x => x.Id.ToString().Equals(id)).ConfigureAwait(false);
            return rowsDeleted > 0;
        }

        public Task<Dose> GetItemAsync(string id)
        {
            return this.foodJournalDatabase.Database.Table<Dose>().FirstOrDefaultAsync(x => x.Id.ToString().Equals(id));
        }

        public Task<List<Dose>> GetItemsAsync(bool forceRefresh = false)
        {
            return this.foodJournalDatabase.Database.Table<Dose>().Take(100).ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(Dose item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.UpdateAsync(item).ConfigureAwait(false);
            return primaryKey > 0;
        }
    }
}
