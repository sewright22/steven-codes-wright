using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels.JournalEntry
{
    public class BloodSugarReadingsViewModel : BaseViewModel
    {
        private readonly IBloodSugarService bloodSugarService;
        private readonly IJournalEntryDetailsService journalEntryDetailsService;
        private readonly IMessagingCenter messagingCenter;

        public BloodSugarReadingsViewModel(IBloodSugarService bloodSugarService, IJournalEntryDetailsService journalEntryDetailsService, IMessagingCenter messagingCenter)
        {
            this.bloodSugarService = bloodSugarService ?? throw new ArgumentNullException(nameof(bloodSugarService));
            this.journalEntryDetailsService = journalEntryDetailsService ?? throw new ArgumentNullException(nameof(journalEntryDetailsService));
            this.messagingCenter = messagingCenter ?? throw new ArgumentNullException(nameof(messagingCenter));
            this.messagingCenter.Subscribe<JournalEntrySummary>(this, "LoadReadings", async (entry) => await this.Refresh(entry));
        }

        public ObservableRangeCollection<ChartReading> OtherEntries { get; } = new ObservableRangeCollection<ChartReading>();
        public ObservableRangeCollection<GlucoseReading> HighReadings { get; } = new ObservableRangeCollection<GlucoseReading>();
        public ObservableRangeCollection<GlucoseReading> BgReadings { get; } = new ObservableRangeCollection<GlucoseReading>();
        public ObservableRangeCollection<GlucoseReading> LowReadings { get; } = new ObservableRangeCollection<GlucoseReading>();

        private async Task Refresh(JournalEntrySummary entry)
        {
            var readingsFromCgm = await this.bloodSugarService.GetCgmReadings(entry.DateLogged.Value).ConfigureAwait(true);

            Device.BeginInvokeOnMainThread(() => this.HighReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading >= 180).OrderBy(x => x.DisplayTime)));
            Device.BeginInvokeOnMainThread(() => this.BgReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading < 180 && x.Reading >= 80).OrderBy(x => x.DisplayTime)));
            Device.BeginInvokeOnMainThread(() => this.LowReadings.ReplaceRange(readingsFromCgm.Where(x => x.Reading < 80).OrderBy(x => x.DisplayTime)));

            this.messagingCenter.Send(new AdvancedBloodSugarStatsArgs(), "BloodSugarReadingsUpdated");
        }
    }
}
