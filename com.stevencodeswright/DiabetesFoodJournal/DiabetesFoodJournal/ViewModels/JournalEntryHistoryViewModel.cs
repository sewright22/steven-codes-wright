using DiabetesFoodJournal.Models;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using DiabetesFoodJournal.Services;
using DiabetesFoodJournal.DataModels;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalEntryHistoryViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;

        public JournalEntryHistoryViewModel(IMessenger messenger)
        {
            this.messenger = messenger;

            JournalEntries = new ObservableRangeCollection<Grouping<string, JournalEntryDataModel>>();

            if (this.messenger != null)
            {
                this.messenger.Register<FoodSearchResult>(this, async (food)=> { await FoodReceived(food); });
            }

        }

        public ObservableRangeCollection<Grouping<string, JournalEntryDataModel>> JournalEntries { get; }

        private async Task FoodReceived(FoodSearchResult food)
        {
            var foodList = new List<JournalEntryDataModel>();

            //foodList.Add(new JournalEntry()
            //{
            //    Id = 1,
            //    FoodName = "Pizza 1",
            //    Logged = new DateTime(2020, 3, 5, 5, 5, 12)
            //});
            //foodList.Add(new JournalEntry()
            //{
            //    Id = 2,
            //    FoodName = "Pizza 1",
            //    Logged = new DateTime(2020, 3, 5, 5, 5, 12)
            //});
            //foodList.Add(new JournalEntry()
            //{
            //    Id = 3,
            //    FoodName = "Pizza 1",
            //    Logged = new DateTime(2020, 4, 5, 5, 5, 12)
            //});
            //foodList.Add(new JournalEntry()
            //{
            //    Id = 4,
            //    FoodName = "Pizza 1",
            //    Logged = new DateTime(2020, 4, 5, 5, 5, 12)
            //});

            var sorted = from entry in foodList
                         orderby entry.Logged descending
                         group entry by entry.Group into entryGroup
                         select new Grouping<string, JournalEntryDataModel>(entryGroup.Key, entryGroup);

            JournalEntries.ReplaceRange(sorted);

            await Task.Run(() => Thread.Sleep(100));
        }

    }
}
