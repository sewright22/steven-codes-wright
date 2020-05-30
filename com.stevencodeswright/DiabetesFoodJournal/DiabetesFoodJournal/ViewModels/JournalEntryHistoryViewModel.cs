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
using Xamarin.Forms;
using GalaSoft.MvvmLight.Command;

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

            GlucoseReadings = new ObservableRangeCollection<GlucoseReading>();
            JournalEntries = new ObservableRangeCollection<JournalEntryDataModel>();
            ItemTappedCommand = new RelayCommand<JournalEntryDataModel>(async (x)=> await ItemTapped(x));
            if (this.messenger != null)
            {
                this.messenger.Register<JournalEntryDataModel>(this, async (x) => await JournalEntryReceived(x));
            }

        }

        private async Task ItemTapped(JournalEntryDataModel foodResult)
        {
            IsBusy = true;

            foodResult.IsSelected = true;
            var readings = await this.dataService.GetGlucoseReadings(foodResult.Logged, foodResult.Logged.AddHours(5)).ConfigureAwait(true);

            Device.BeginInvokeOnMainThread(() => GlucoseReadings.ReplaceRange(readings.OrderBy(x=>x.DisplayTime)));
            IsBusy = false;
        }

        public RelayCommand<JournalEntryDataModel> ItemTappedCommand { get; }

        public ObservableRangeCollection<GlucoseReading> GlucoseReadings { get; }

        public ObservableRangeCollection<JournalEntryDataModel> JournalEntries { get; }

        private async Task JournalEntryReceived(JournalEntryDataModel searchEntry)
        {
            var entryList = await this.dataService.SearchJournal(searchEntry.Title).ConfigureAwait(true);

            Device.BeginInvokeOnMainThread(()=> JournalEntries.ReplaceRange(entryList));
        }
    }
}
