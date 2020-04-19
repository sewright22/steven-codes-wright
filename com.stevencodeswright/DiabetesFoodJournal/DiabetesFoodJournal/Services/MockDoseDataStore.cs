using DiabetesFoodJournal.ModelLinks;
using DiabetesFoodJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class MockDoseDataStore : IDataStore<Dose>
    {
        readonly List<Dose> items;

        public MockDoseDataStore()
        {
            items = new List<Dose>();

            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=7.54M, UpFront = 75, Extended = 25, TimeExtended = 2, TimeOffset = -10 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
            Add(new Dose() { InsulinAmount=32, UpFront = 100, Extended = 0, TimeExtended = 0, TimeOffset = 0 });
        }

        private void Add(Dose item)
        {
            if (item.Id == 0)
            {
                item.Id = items.Count + 1;
            }

            items.Add(item);
        }

        public async Task<bool> AddItemAsync(Dose item)
        {
            Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Dose arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Dose> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<Dose>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(Dose item)
        {
            var oldItem = items.Where((Dose arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
