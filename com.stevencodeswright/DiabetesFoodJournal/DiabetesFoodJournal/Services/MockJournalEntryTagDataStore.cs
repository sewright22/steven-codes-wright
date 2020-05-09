using DiabetesFoodJournal.ModelLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class MockJournalEntryTagDataStore : IDataStore<JournalEntryTag>
    {
        readonly List<JournalEntryTag> items;

        public MockJournalEntryTagDataStore()
        {
            items = new List<JournalEntryTag>();

            Add(new JournalEntryTag() { JournalEntryId = 1, TagId = 1 });
            Add(new JournalEntryTag() { JournalEntryId = 1, TagId = 3 });
            Add(new JournalEntryTag() { JournalEntryId = 1, TagId = 13 });
            Add(new JournalEntryTag() { JournalEntryId = 2, TagId = 4 });
            Add(new JournalEntryTag() { JournalEntryId = 2, TagId = 13 });
            Add(new JournalEntryTag() { JournalEntryId = 2, TagId = 10 });
        }

        private void Add(JournalEntryTag item)
        {
            if (item.Id == 0)
            {
                item.Id = items.Count + 1;
            }

            items.Add(item);
        }

        public async Task<bool> AddItemAsync(JournalEntryTag item)
        {
            Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((JournalEntryTag arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<JournalEntryTag> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<List<JournalEntryTag>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(JournalEntryTag item)
        {
            var oldItem = items.Where((JournalEntryTag arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
