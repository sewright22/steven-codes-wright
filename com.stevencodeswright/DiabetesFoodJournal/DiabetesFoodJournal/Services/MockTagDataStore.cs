using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.ModelLinks;
using DiabetesFoodJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class MockTagDataStore : IDataStore<Tag>
    {
        readonly List<Tag> items;

        public MockTagDataStore()
        {
            items = new List<Tag>();

            Add(new Tag() { Description = "Papa Johns" });
            Add(new Tag() { Description = "Pizza Hut" });
            Add(new Tag() { Description = "Pepperoni" });
            Add(new Tag() { Description = "McDonalds" });
            Add(new Tag() { Description = "Wendys" });
            Add(new Tag() { Description = "Chicken Nuggets" });
            Add(new Tag() { Description = "Vanilla" });
            Add(new Tag() { Description = "Sonic" });
            Add(new Tag() { Description = "Chocolate" });
            Add(new Tag() { Description = "Egg" });
            Add(new Tag() { Description = "Cheese" });
            Add(new Tag() { Description = "Sausage" });
            Add(new Tag() { Description = "Bacon" });
            Add(new Tag() { Description = "Large" });
            Add(new Tag() { Description = "Medium" });
            Add(new Tag() { Description = "Small" });
            Add(new Tag() { Description = "Strawberry" });
        }

        private void Add(Tag item)
        {
            if (item.Id == 0)
            {
                item.Id = items.Count + 1;
            }

            items.Add(item);
        }

        public async Task<bool> AddItemAsync(Tag item)
        {
            Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Tag arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Tag> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<List<Tag>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(Tag item)
        {
            var oldItem = items.Where((Tag arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
