﻿using DiabetesFoodJournal.Data;
using DiabetesFoodJournal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class LocalNutritionalInfoDataStore : IDataStore<NutritionalInfo>
    {
        private readonly IFoodJournalDatabase foodJournalDatabase;

        public LocalNutritionalInfoDataStore(IFoodJournalDatabase foodJournalDatabase)
        {
            this.foodJournalDatabase = foodJournalDatabase;
        }
        public async Task<int> AddItemAsync(NutritionalInfo item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.InsertAsync(item).ConfigureAwait(false);
            return item.Id;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var rowsDeleted = await this.foodJournalDatabase.Database.Table<NutritionalInfo>().DeleteAsync(x => x.Id.ToString().Equals(id)).ConfigureAwait(false);
            return rowsDeleted > 0;
        }

        public Task<NutritionalInfo> GetItemAsync(string id)
        {
            return this.foodJournalDatabase.Database.Table<NutritionalInfo>().FirstOrDefaultAsync(x => x.Id.ToString().Equals(id));
        }

        public Task<List<NutritionalInfo>> GetItemsAsync(bool forceRefresh = false)
        {
            return this.foodJournalDatabase.Database.Table<NutritionalInfo>().Take(100).ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(NutritionalInfo item)
        {
            var primaryKey = await this.foodJournalDatabase.Database.UpdateAsync(item).ConfigureAwait(false);
            return primaryKey > 0;
        }
    }
}
