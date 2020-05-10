using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class MockNutritionalInfoDataStore : IDataStore<NutritionalInfo>
    {
        readonly List<NutritionalInfo> items;

        public MockNutritionalInfoDataStore()
        {
            items = new List<NutritionalInfo>();

            Add(new NutritionalInfo() { Carbohydrates=72 });
            Add(new NutritionalInfo() { Carbohydrates=25 });
            Add(new NutritionalInfo() { Carbohydrates=64 });
            Add(new NutritionalInfo() { Carbohydrates=45 });
            Add(new NutritionalInfo() { Carbohydrates=65 });
            Add(new NutritionalInfo() { Carbohydrates=22 });
            Add(new NutritionalInfo() { Carbohydrates=12 });
            Add(new NutritionalInfo() { Carbohydrates=37 });
            Add(new NutritionalInfo() { Carbohydrates=54 });
            Add(new NutritionalInfo() { Carbohydrates=11 });
            Add(new NutritionalInfo() { Carbohydrates=87 });
            Add(new NutritionalInfo() { Carbohydrates=55 });
            Add(new NutritionalInfo() { Carbohydrates=15 });
            Add(new NutritionalInfo() { Carbohydrates=39 });
            Add(new NutritionalInfo() { Carbohydrates=21 });
            Add(new NutritionalInfo() { Carbohydrates=28 });
            Add(new NutritionalInfo() { Carbohydrates=23 });
            Add(new NutritionalInfo() { Carbohydrates=76 });
        }

        private int Add(NutritionalInfo item)
        {
            if (item.Id == 0)
            {
                item.Id = items.Count + 1;
            }

            items.Add(item);

            return item.Id;
        }

        public async Task<int> AddItemAsync(NutritionalInfo item)
        {
            item.Id = Add(item);

            return await Task.FromResult(item.Id);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((NutritionalInfo arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<NutritionalInfo> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<List<NutritionalInfo>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(NutritionalInfo item)
        {
            var oldItem = items.Where((NutritionalInfo arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
