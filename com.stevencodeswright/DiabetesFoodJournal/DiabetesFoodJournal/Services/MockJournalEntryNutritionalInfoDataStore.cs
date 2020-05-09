using DiabetesFoodJournal.ModelLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class MockJournalEntryNutritionalInfoDataStore : IDataStore<JournalEntryNutritionalInfo>
    {
        readonly List<JournalEntryNutritionalInfo> items;

        public MockJournalEntryNutritionalInfoDataStore()
        {
            items = new List<JournalEntryNutritionalInfo>();

            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 1 , JournalEntryNutritionalInfoId = 1  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 2 , JournalEntryNutritionalInfoId = 2  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 3 , JournalEntryNutritionalInfoId = 3  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 4 , JournalEntryNutritionalInfoId = 4  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 5 , JournalEntryNutritionalInfoId = 5  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 6 , JournalEntryNutritionalInfoId = 6  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 7 , JournalEntryNutritionalInfoId = 7  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 8 , JournalEntryNutritionalInfoId = 8  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 9 , JournalEntryNutritionalInfoId = 9  });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 10, JournalEntryNutritionalInfoId = 10 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 11, JournalEntryNutritionalInfoId = 11 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 12, JournalEntryNutritionalInfoId = 12 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 13, JournalEntryNutritionalInfoId = 13 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 14, JournalEntryNutritionalInfoId = 14 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 15, JournalEntryNutritionalInfoId = 15 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 16, JournalEntryNutritionalInfoId = 16 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 17, JournalEntryNutritionalInfoId = 17 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 18, JournalEntryNutritionalInfoId = 18 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 19, JournalEntryNutritionalInfoId = 19 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 20, JournalEntryNutritionalInfoId = 20 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 21, JournalEntryNutritionalInfoId = 21 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 22, JournalEntryNutritionalInfoId = 22 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 23, JournalEntryNutritionalInfoId = 23 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 24, JournalEntryNutritionalInfoId = 24 });
            Add(new JournalEntryNutritionalInfo() { JournalEntryId = 25, JournalEntryNutritionalInfoId = 25 });
        }

        private int Add(JournalEntryNutritionalInfo item)
        {
            if (item.Id == 0)
            {
                item.Id = items.Count + 1;
            }

            items.Add(item);

            return item.Id;
        }

        public async Task<int> AddItemAsync(JournalEntryNutritionalInfo item)
        {
            item.Id = Add(item);

            return await Task.FromResult(item.Id);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((JournalEntryNutritionalInfo arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<JournalEntryNutritionalInfo> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<List<JournalEntryNutritionalInfo>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(JournalEntryNutritionalInfo item)
        {
            var oldItem = items.Where((JournalEntryNutritionalInfo arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
