using DiabetesFoodJournal.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels.JournalEntry
{
    public class JournalEntryDetailsViewModel : BaseViewModel
    {
        private readonly IMessagingCenter messagingCenter;
        private readonly IJournalEntryDetailsService journalEntryDetailsService;
        private JournalEntryDetails model;

        public JournalEntryDetailsViewModel(IMessagingCenter messagingCenter, IJournalEntryDetailsService journalEntryDetailsService)
        {
            this.messagingCenter = messagingCenter ?? throw new ArgumentNullException(nameof(messagingCenter));
            this.journalEntryDetailsService = journalEntryDetailsService ?? throw new ArgumentNullException(nameof(journalEntryDetailsService));
            this.messagingCenter.Subscribe<JournalEntrySummary>(this, "LoadReadings", async (entry) => await this.Refresh(entry));
            this.messagingCenter.Subscribe<JournalEntryViewModel>(this, "LogAgainClicked", (entry) => this.messagingCenter.Send(this.Model, "LogAgainClicked"));
        }

        private async Task Refresh(JournalEntrySummary entry)
        {
            this.Model = await this.journalEntryDetailsService.GetDetails(entry.ID).ConfigureAwait(false);
        }

        public JournalEntryDetails Model
        {
            get { return this.model; }
            set { this.SetProperty(ref this.model, value); }
        }
    }
}
