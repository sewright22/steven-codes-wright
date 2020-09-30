using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels
{
    public class BgReadingsViewModel : BaseViewModel
    {
        private readonly IBgReadingsDataService dataService;
        private readonly IMessenger messenger;
        private readonly INavigationHelper navigation;
        private JournalEntryDataModel model;
        private JournalEntryDataModel logAgainModel;

        public BgReadingsViewModel(IBgReadingsDataService dataService, IMessenger messenger, INavigationHelper navigation)
        {
            this.dataService = dataService;
            this.messenger = messenger;
            this.navigation = navigation;
            this.LogAgainCommand = new RelayCommand(this.LogAgainClicked);
            this.LogCommand = new AsyncCommand(this.LogClicked);

            if (this.messenger != null)
            {
                this.messenger.Register<JournalEntryDataModel>(this, async (model) => await JournalEntryReceived(model));
            }
        }

        private void LogAgainClicked()
        {
            this.LogAgainModel = this.dataService.Copy(this.Model);
        }

        private async Task LogClicked()
        {
            await this.navigation.GoToAsync($"journalEntry").ConfigureAwait(false);
            this.LogAgainModel.Logged = DateTime.Now;

            this.LogAgainModel = await this.dataService.SaveEntry(this.LogAgainModel).ConfigureAwait(false);
            if (this.LogAgainModel.Id > 0)
            {
                this.messenger.Send(this.LogAgainModel);
            }
        }

        public ObservableRangeCollection<ChartReading> OtherEntries { get; } = new ObservableRangeCollection<ChartReading>();

        public JournalEntryDataModel Model { get { return this.model; } set { SetProperty(ref this.model, value); } }
        public JournalEntryDataModel LogAgainModel 
        { 
            get { return this.logAgainModel; } 
            set { SetProperty(ref this.logAgainModel, value); } 
        }
        public RelayCommand LogAgainCommand { get; set; }
        public AsyncCommand LogCommand { get; set; }

        private async Task JournalEntryReceived(JournalEntryDataModel model)
        {
            this.Model = model;
            this.Title = this.Model.Title;

            if (this.Model.BgReadings.Any() == false)
            {
                IsBusy = true;
                try
                {
                    var readingsFromCgm = await this.dataService.GetCgmReadings(this.model.Logged).ConfigureAwait(true);
                    var otherEntries = await this.dataService.GetOtherEntries(this.model.Logged, this.model.Id);

                    if (readingsFromCgm.Any())
                    {
                        Device.BeginInvokeOnMainThread(() => this.Model.HighReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading >= 180).OrderBy(x => x.DisplayTime)));
                        Device.BeginInvokeOnMainThread(() => this.Model.BgReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading < 180 && x.Reading >= 80).OrderBy(x => x.DisplayTime)));
                        Device.BeginInvokeOnMainThread(() => this.Model.LowReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading < 80).OrderBy(x => x.DisplayTime)));
                        Device.BeginInvokeOnMainThread(() => this.Model.StartingBg = this.Model.BgReadings.FirstOrDefault().Reading);
                        await AnalyzeReadings(readingsFromCgm);
                    }

                    if(otherEntries.Any())
                    {
                        Device.BeginInvokeOnMainThread(() => this.OtherEntries.ReplaceRange(otherEntries.Where(x=>x.DisplayTime!=0).OrderBy(x => x.DisplayTime)));
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() => this.OtherEntries.Clear());
                    }
                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(() => this.Model.BgReadings.Clear());
                }
            }

            IsBusy = false;
        }

        private async Task AnalyzeReadings(IEnumerable<GlucoseReading> readingsFromCgm)
        {
            var highReading = await Task.Run(() => readingsFromCgm.Aggregate((r1, r2) => r1.Reading >= r2.Reading ? r1 : r2));
            var lowReading = await Task.Run(() => readingsFromCgm.Aggregate((r1, r2) => r1.Reading <= r2.Reading ? r1 : r2));

            Device.BeginInvokeOnMainThread(() => this.Model.HighestBg = highReading.Reading);
            Device.BeginInvokeOnMainThread(() => this.Model.HighestBgTimeSpanInMinutes = highReading.DisplayTime);
            Device.BeginInvokeOnMainThread(() => this.Model.LowestBg = lowReading.Reading);
            Device.BeginInvokeOnMainThread(() => this.Model.LowestBgTimeSpanInMinutes = lowReading.DisplayTime);
        }
    }
}
