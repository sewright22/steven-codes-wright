using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalViewModel : BaseViewModel
    {
        private readonly IJournalDataService dataService;
        private readonly INavigationHelper navigation;
        private readonly IMessenger messenger;

        public JournalViewModel(IJournalDataService dataService, INavigationHelper navigation, IMessenger messenger)
        {
            LocalSearchResults = new ObservableRangeCollection<Grouping<string, JournalEntryDataModel>>();
            SearchCommand = new RelayCommand<string>(async x => await SearchClicked(x).ConfigureAwait(true));
            ItemTappedCommand = new RelayCommand<JournalEntryDataModel>(async x => await ItemTapped(x).ConfigureAwait(true));
            this.dataService = dataService;
            this.navigation = navigation;
            this.messenger = messenger;
        }

        private async Task ItemTapped(JournalEntryDataModel foodResult)
        {
           await this.navigation.GoToAsync($"journalEntry").ConfigureAwait(false);
            this.messenger.Send(foodResult);
        }

        public ObservableRangeCollection<Grouping<string, JournalEntryDataModel>> LocalSearchResults { get; }

        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand<JournalEntryDataModel> ItemTappedCommand { get; set; }

        private async Task SearchClicked(string searchString)
        {
            var entryList = await this.dataService.SearchJournal(searchString);

            var sorted = from entry in entryList
                         orderby entry.Logged descending
                         group entry by entry.Group into entryGroup
                         select new Grouping<string, JournalEntryDataModel>(entryGroup.Key, entryGroup);

            LocalSearchResults.ReplaceRange(sorted);
        }
    }
}
