using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using DiabetesFoodJournal.ViewModels.Journal;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels
{
    public class BgReadingsViewModel : BaseViewModel
    {
        private readonly IBgReadingsDataService dataService;
        private readonly IMessagingCenter messagingCenter;
        private readonly IMessenger messenger;
        private readonly INavigationHelper navigation;
        private readonly IJournalEntrySummaryService journalEntrySummaryService;
        private JournalEntrySummary model;
        private JournalEntryDataModel logAgainModel;
        private string tags;
        private float? startingBg;
        private float? highestBg;
        private float? lowestBg;
        private int? highestBgTimeSpanInMinutes;
        private int? lowestBgTimeSpanInMinutes;

        public BgReadingsViewModel(IBgReadingsDataService dataService, IMessagingCenter messagingCenter, INavigationHelper navigation, IJournalEntrySummaryService journalEntrySummaryService)
        {
            this.dataService = dataService;
            this.messagingCenter = messagingCenter;
            this.navigation = navigation;
            this.journalEntrySummaryService = journalEntrySummaryService ?? throw new ArgumentNullException(nameof(journalEntrySummaryService));
            this.LogAgainCommand = new RelayCommand(this.LogAgainClicked);
            this.LogCommand = new AsyncCommand(this.LogClicked);
            this.messagingCenter.Subscribe<JournalEntrySummary>(this, "LoadReadings", async (s) => await this.Refresh(s));
        }

        private void LogAgainClicked()
        {
            //this.LogAgainModel = this.dataService.Copy(this.Model);
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
        public ObservableRangeCollection<GlucoseReading> HighReadings { get; } = new ObservableRangeCollection<GlucoseReading>();
        public ObservableRangeCollection<GlucoseReading> BgReadings { get; } = new ObservableRangeCollection<GlucoseReading>();
        public ObservableRangeCollection<GlucoseReading> LowReadings { get; } = new ObservableRangeCollection<GlucoseReading>();
        
        public float? StartingBg { get { return this.startingBg; } set { SetProperty(ref this.startingBg, value); } }
        public float? HighestBg { get { return this.highestBg; } set { SetProperty(ref this.highestBg, value); } }
        public int? HighestBgTimeSpanInMinutes { get { return this.highestBgTimeSpanInMinutes; } set { SetProperty(ref this.highestBgTimeSpanInMinutes, value); } }
        public float? LowestBg { get { return this.lowestBg; } set { SetProperty(ref this.lowestBg, value); } }
        public int? LowestBgTimeSpanInMinutes { get { return this.lowestBgTimeSpanInMinutes; } set { SetProperty(ref this.lowestBgTimeSpanInMinutes, value); } }

        public JournalEntrySummary Model { get { return this.model; } set { SetProperty(ref this.model, value); } }
        public string Tags { get { return this.tags; } set { SetProperty(ref this.tags, value); } }
        public JournalEntryDataModel LogAgainModel 
        { 
            get { return this.logAgainModel; } 
            set { SetProperty(ref this.logAgainModel, value); } 
        }
        public RelayCommand LogAgainCommand { get; set; }
        public AsyncCommand LogCommand { get; set; }

        private async Task AnalyzeReadings(IEnumerable<GlucoseReading> readingsFromCgm)
        {
            var highReading = await Task.Run(() => readingsFromCgm.Aggregate((r1, r2) => r1.Reading >= r2.Reading ? r1 : r2));
            var lowReading = await Task.Run(() => readingsFromCgm.Aggregate((r1, r2) => r1.Reading <= r2.Reading ? r1 : r2));

            Device.BeginInvokeOnMainThread(() => this.HighestBg = highReading.Reading);
            Device.BeginInvokeOnMainThread(() => this.HighestBgTimeSpanInMinutes = highReading.DisplayTime);
            Device.BeginInvokeOnMainThread(() => this.LowestBg = lowReading.Reading);
            Device.BeginInvokeOnMainThread(() => this.LowestBgTimeSpanInMinutes = lowReading.DisplayTime);
        }

        public async Task Refresh(JournalEntrySummary journalEntrySummary)
        {
            IsBusy = true;
            try
            {
                this.Model = journalEntrySummary;
                if (model != null)
                {
                    this.Title = model.Title;
                    this.Tags = model.Tags;

                    var readingsFromCgm = await this.dataService.GetCgmReadings(model.DateLogged.Value).ConfigureAwait(true);
                    var otherEntries = await this.dataService.GetOtherEntries(model.DateLogged.Value, model.ID);

                    Device.BeginInvokeOnMainThread(() => this.HighReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading >= 180).OrderBy(x => x.DisplayTime)));
                    Device.BeginInvokeOnMainThread(() => this.BgReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading < 180 && x.Reading >= 80).OrderBy(x => x.DisplayTime)));
                    Device.BeginInvokeOnMainThread(() => this.LowReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading < 80).OrderBy(x => x.DisplayTime)));
                    await this.AnalyzeReadings(readingsFromCgm);

                    if (otherEntries.Any())
                    {
                        Device.BeginInvokeOnMainThread(() => this.OtherEntries.ReplaceRange(otherEntries.Where(x => x.DisplayTime != 0).OrderBy(x => x.DisplayTime)));
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() => this.OtherEntries.Clear());
                    }
                }
            }
            catch (Exception)
            {
                Device.BeginInvokeOnMainThread(() => this.BgReadings.Clear());
            }

            this.IsBusy = false;
        }
    }
}
