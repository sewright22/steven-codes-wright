using DiabetesFoodJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class MockJournalEntryDataStore : IDataStore<JournalEntry>
    {
        readonly List<JournalEntry> items;

        public MockJournalEntryDataStore()
        {
            items = new List<JournalEntry>();
            Add(new JournalEntry { Title = "Pizza", Logged = new DateTime(2020, 2, 15, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Breakfast Burrito", Logged = new DateTime(2020, 3, 15, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Cake", Logged = new DateTime(2020, 3, 15, 7, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Ice Cream", Logged = new DateTime(2020, 3, 15, 8, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Pizza Rolls", Logged = new DateTime(2020, 3, 15, 9, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Spaghetti", Logged = new DateTime(2020, 3, 15, 13, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Chik Fil A Chicken Nuggets", Logged = new DateTime(2020, 3, 15, 15, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Papa Johns Pizza", Logged = new DateTime(2020, 3, 15, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Burger", Logged = new DateTime(2019, 4, 9, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Cornbread", Logged = new DateTime(2019, 12, 15, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Lamb Dinner", Logged = new DateTime(2019, 12, 16, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Dinner at Parents", Logged = new DateTime(2019, 12, 17, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Chili's Chicken Strips", Logged = new DateTime(2019, 12, 18, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Salad", Logged = new DateTime(2019, 12, 19, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Milkshake", Logged = new DateTime(2019, 12, 20, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Coffee", Logged = new DateTime(2019, 12, 21, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Soylent", Logged = new DateTime(2019, 12, 22, 16, 42, 24), Notes="" });
            Add(new JournalEntry { Title = "Pizza", Logged = new DateTime(2019, 12, 23, 16, 42, 24), Notes = "" });
        }

        private void Add(JournalEntry item)
        {
            if (item.Id == 0)
            {
                item.Id = items.Count + 1;
            }

            items.Add(item);
        }


        public async Task<bool> AddItemAsync(JournalEntry item)
        {
            Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(JournalEntry item)
        {
            var oldItem = items.Where((JournalEntry arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((JournalEntry arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<JournalEntry> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<JournalEntry>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
