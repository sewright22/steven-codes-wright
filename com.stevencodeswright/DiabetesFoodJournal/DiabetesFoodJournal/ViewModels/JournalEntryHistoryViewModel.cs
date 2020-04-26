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
using DiabetesFoodJournal.DataServices;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalEntryHistoryViewModel : BaseViewModel
    {
        private readonly IJournalEntryHistoryDataService dataService;
        private readonly IMessenger messenger;

        public JournalEntryHistoryViewModel(IJournalEntryHistoryDataService dataService, IMessenger messenger)
        {
            this.dataService = dataService;
            this.messenger = messenger;

            JournalEntries = new ObservableRangeCollection<Grouping<string, JournalEntryDataModel>>();

            if (this.messenger != null)
            {
                this.messenger.Register<JournalEntryDataModel>(this, async (x) => await JournalEntryReceived(x));
            }

        }

        public ObservableRangeCollection<Grouping<string, JournalEntryDataModel>> JournalEntries { get; }

        private async Task JournalEntryReceived(JournalEntryDataModel searchEntry)
        {
            var entryList = await this.dataService.SearchJournal(searchEntry.Title);

            JournalEntries.ReplaceRange(entryList);
        }
    }
}
