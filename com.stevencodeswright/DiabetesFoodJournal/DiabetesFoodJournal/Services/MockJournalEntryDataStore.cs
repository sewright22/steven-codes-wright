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
            items.Add(new JournalEntry { Id = 1,  Title = "Pizza", Logged = new DateTime(2020, 2, 15, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 2,  Title = "Breakfast Burrito", Logged = new DateTime(2020, 3, 15, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 2,  Title = "Cake", Logged = new DateTime(2020, 3, 15, 7, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 2,  Title = "Ice Cream", Logged = new DateTime(2020, 3, 15, 8, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 2,  Title = "Pizza Rolls", Logged = new DateTime(2020, 3, 15, 9, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 2,  Title = "Spaghetti", Logged = new DateTime(2020, 3, 15, 13, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 2,  Title = "Chik Fil A Chicken Nuggets", Logged = new DateTime(2020, 3, 15, 15, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 2,  Title = "Papa Johns Pizza", Logged = new DateTime(2020, 3, 15, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 3,  Title = "Burger", Logged = new DateTime(2019, 4, 9, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 4,  Title = "Cornbread", Logged = new DateTime(2019, 12, 15, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 5,  Title = "Lamb Dinner", Logged = new DateTime(2019, 12, 16, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 6,  Title = "Dinner at Parents", Logged = new DateTime(2019, 12, 17, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 7,  Title = "Chili's Chicken Strips", Logged = new DateTime(2019, 12, 18, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 8,  Title = "Salad", Logged = new DateTime(2019, 12, 19, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 9,  Title = "Milkshake", Logged = new DateTime(2019, 12, 20, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 10, Title = "Coffee", Logged = new DateTime(2019, 12, 21, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 11, Title = "Soylent", Logged = new DateTime(2019, 12, 22, 16, 42, 24), Notes="" });
            items.Add(new JournalEntry { Id = 12, Title = "Pizza", Logged = new DateTime(2019, 12, 23, 16, 42, 24), Notes = "" });
        }


        public async Task<bool> AddItemAsync(JournalEntry item)
        {
            items.Add(item);

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
