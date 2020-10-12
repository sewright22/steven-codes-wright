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
    public class AdvancedBloodSugarStatsViewModel : BaseViewModel
    {
        private readonly IBloodSugarService bloodSugarService;
        private readonly IMessagingCenter messagingCenter;
        private AdvancedBloodSugarStats model;

        public AdvancedBloodSugarStatsViewModel(IBloodSugarService bloodSugarService, IMessagingCenter messagingCenter)
        {
            this.bloodSugarService = bloodSugarService ?? throw new ArgumentNullException(nameof(bloodSugarService));
            this.messagingCenter = messagingCenter ?? throw new ArgumentNullException(nameof(messagingCenter));

            this.messagingCenter.Subscribe<AdvancedBloodSugarStatsArgs>(this, "BloodSugarReadingsUpdated", async (args) => await this.BloodSugarReadingsUpdated(args));
        }

        public AdvancedBloodSugarStats Model
        {
            get { return this.model; }
            set { this.SetProperty(ref this.model, value); }
        }

        private async Task BloodSugarReadingsUpdated(AdvancedBloodSugarStatsArgs args)
        {
            this.Model = await this.bloodSugarService.GetAdvancedStats().ConfigureAwait(true);
        }
    }
}
