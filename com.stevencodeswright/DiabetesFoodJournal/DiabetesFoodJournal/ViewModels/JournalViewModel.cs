using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.ViewModels.Journal;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalViewModel : BaseViewModel
    {
        private readonly IJournalDataService dataService;
        private readonly INavigationHelper navigation;
        private readonly IMessenger messenger;
        private bool rowIsSelected;
        private bool refreshing;
        private bool searching;

        public JournalViewModel(IJournalDataService dataService, INavigationHelper navigation, IMessenger messenger)
        {
            LocalSearchResults = new ObservableRangeCollection<Grouping<string, JournalEntrySummaryViewModel>>();
            SearchCommand = new AsyncCommand<string>(SearchClicked);
            UpdateEntryCommand = new RelayCommand(async () => await UpdateClicked().ConfigureAwait(true));
            LogAgainCommand = new RelayCommand(async () => await LogAgainClicked().ConfigureAwait(true));
            ItemTappedCommand = new RelayCommand<JournalEntrySummaryViewModel>(x => ItemTapped(x));
            CreateNewEntryCommand = new RelayCommand<string>(async entryTitle => await CreateNewEntryClicked(entryTitle).ConfigureAwait(true));
            ViewReadingsCommand = new AsyncCommand(this.ViewReadingsClicked);
            this.dataService = dataService;
            this.navigation = navigation;
            this.messenger = messenger;
        }

        private async Task ViewReadingsClicked()
        {
            await this.navigation.GoToAsync($"BgReadings").ConfigureAwait(false);
            this.messenger.Send(SelectedEntry);
        }

        private async Task LogAgainClicked()
        {
            await this.navigation.GoToAsync($"journalEntry").ConfigureAwait(false);
            //var entry = this.dataService.Copy(SelectedEntry);
            //entry.Logged = DateTime.Now;

            //entry = await this.dataService.SaveEntry(entry);
            //if(entry.Id>0)
            //{
            //    this.messenger.Send(entry);
            //}
        }

        private async Task CreateNewEntryClicked(string entryTitle)
        {
            await this.navigation.GoToAsync($"journalEntry").ConfigureAwait(true);
            var newEntry = this.dataService.CreateEntry(entryTitle);
            newEntry = await this.dataService.SaveEntry(newEntry);

            if (newEntry.Id > 0)
            {
                this.messenger.Send(newEntry);
                await SearchClicked(entryTitle);
            }
        }

        private async Task UpdateClicked()
        {
            await this.navigation.GoToAsync($"journalEntry").ConfigureAwait(false);
            this.messenger.Send(SelectedEntry);
        }

        private void ItemTapped(JournalEntrySummaryViewModel foodResult)
        {
            foreach(var group in LocalSearchResults)
            {
                foreach(var item in group.Items)
                {
                    item.IsSelected = false;
                }
            }

            foodResult.IsSelected = true;
            SelectedEntry = foodResult;
            RowIsSelected = true;
        }

        public bool Refreshing { get { return this.refreshing; } set { SetProperty(ref this.refreshing, value); } }
        public bool RowIsSelected { get { return this.rowIsSelected; } set { SetProperty(ref this.rowIsSelected, value); } }
        public JournalEntrySummaryViewModel SelectedEntry { get; set; }

        public ObservableRangeCollection<Grouping<string, JournalEntrySummaryViewModel>> LocalSearchResults { get; }

        public AsyncCommand<string> SearchCommand { get; set; }
        public RelayCommand<string> CreateNewEntryCommand { get; set; }
        public RelayCommand LogAgainCommand { get; set; }
        public RelayCommand UpdateEntryCommand { get; set; }
        public AsyncCommand ViewReadingsCommand { get; set; }
        public RelayCommand<JournalEntrySummaryViewModel> ItemTappedCommand { get; set; }

        private async Task SearchClicked(string searchString)
        {
            if (!this.searching)
            {
                this.searching = true;
                Refreshing = true;
                var tempSelected = this.SelectedEntry;
                var entryList = await this.dataService.SearchJournal(searchString, false).ConfigureAwait(true);

                var entryViewModelList = new List<JournalEntrySummaryViewModel>();

                var sorted = from entry in entryList
                             orderby entry.Model.DateLogged descending
                             group entry by entry.Group into entryGroup
                             select new Grouping<string, JournalEntrySummaryViewModel>(entryGroup.Key, entryGroup);

                RowIsSelected = false;
                Refreshing = false;
                this.searching = false;
                LocalSearchResults.ReplaceRange(sorted);
                if (tempSelected != null)
                {
                    var selectedEntry = entryList.FirstOrDefault(e => e.Model.ID == tempSelected.Model.ID);
                    this.SelectedEntry = selectedEntry;
                    this.SelectedEntry.IsSelected = true;
                }
            }
        }
    }
}
