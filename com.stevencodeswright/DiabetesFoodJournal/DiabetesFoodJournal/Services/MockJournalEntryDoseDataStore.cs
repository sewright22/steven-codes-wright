using DiabetesFoodJournal.ModelLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class MockJournalEntryDoseDataStore : IDataStore<JournalEntryDose>
    {
        readonly List<JournalEntryDose> items;

        public MockJournalEntryDoseDataStore()
        {
            items = new List<JournalEntryDose>();

            Add(new JournalEntryDose() { JournalEntryId = 1 , DoseId = 1  });
            Add(new JournalEntryDose() { JournalEntryId = 2 , DoseId = 2  });
            Add(new JournalEntryDose() { JournalEntryId = 3 , DoseId = 3  });
            Add(new JournalEntryDose() { JournalEntryId = 4 , DoseId = 4  });
            Add(new JournalEntryDose() { JournalEntryId = 5 , DoseId = 5  });
            Add(new JournalEntryDose() { JournalEntryId = 6 , DoseId = 6  });
            Add(new JournalEntryDose() { JournalEntryId = 7 , DoseId = 7  });
            Add(new JournalEntryDose() { JournalEntryId = 8 , DoseId = 8  });
            Add(new JournalEntryDose() { JournalEntryId = 9 , DoseId = 9  });
            Add(new JournalEntryDose() { JournalEntryId = 10, DoseId = 10 });
            Add(new JournalEntryDose() { JournalEntryId = 11, DoseId = 11 });
            Add(new JournalEntryDose() { JournalEntryId = 12, DoseId = 12 });
            Add(new JournalEntryDose() { JournalEntryId = 13, DoseId = 13 });
            Add(new JournalEntryDose() { JournalEntryId = 14, DoseId = 14 });
            Add(new JournalEntryDose() { JournalEntryId = 15, DoseId = 15 });
            Add(new JournalEntryDose() { JournalEntryId = 16, DoseId = 16 });
            Add(new JournalEntryDose() { JournalEntryId = 17, DoseId = 17 });
            Add(new JournalEntryDose() { JournalEntryId = 18, DoseId = 18 });
            Add(new JournalEntryDose() { JournalEntryId = 19, DoseId = 19 });
            Add(new JournalEntryDose() { JournalEntryId = 20, DoseId = 20 });
            Add(new JournalEntryDose() { JournalEntryId = 21, DoseId = 21 });
            Add(new JournalEntryDose() { JournalEntryId = 22, DoseId = 22 });
            Add(new JournalEntryDose() { JournalEntryId = 23, DoseId = 23 });
            Add(new JournalEntryDose() { JournalEntryId = 24, DoseId = 24 });
            Add(new JournalEntryDose() { JournalEntryId = 25, DoseId = 25 });
        }

        private void Add(JournalEntryDose item)
        {
            if (item.Id == 0)
            {
                item.Id = items.Count + 1;
            }

            items.Add(item);
        }

        public async Task<bool> AddItemAsync(JournalEntryDose item)
        {
            Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((JournalEntryDose arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<JournalEntryDose> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<List<JournalEntryDose>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(JournalEntryDose item)
        {
            var oldItem = items.Where((JournalEntryDose arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
