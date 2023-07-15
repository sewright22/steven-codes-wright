using DiabetesFoodJournal.Data;
using DiabetesFoodJournal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class LocalTagDataStore : IDataStore<Tag>
    {
        private readonly IFoodJournalDatabase foodJournalDatabase;

        public LocalTagDataStore(IFoodJournalDatabase foodJournalDatabase)
        {
            this.foodJournalDatabase = foodJournalDatabase;
        }
        public async Task<int> AddItemAsync(Tag item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.InsertAsync(item).ConfigureAwait(false);
            return item.Id;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var rowsDeleted = await this.foodJournalDatabase.Database.Table<Tag>().DeleteAsync(x => x.Id.ToString().Equals(id)).ConfigureAwait(false);
            return rowsDeleted > 0;
        }

        public Task<Tag> GetItemAsync(string id)
        {
            return this.foodJournalDatabase.Database.Table<Tag>().FirstOrDefaultAsync(x => x.Id.ToString().Equals(id));
        }

        public Task<List<Tag>> GetItemsAsync(bool forceRefresh = false)
        {
            return this.foodJournalDatabase.Database.Table<Tag>().Take(100).ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(Tag item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.UpdateAsync(item).ConfigureAwait(false);
            return primaryKey > 0;
        }
    }
}
