using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels.JournalEntry
{
    public class LogAgainViewModel : BaseViewModel
    {
        private readonly IMessagingCenter messagingCenter;
        private decimal insulinAmount;

        public LogAgainViewModel(IMessagingCenter messagingCenter)
        {
            this.messagingCenter = messagingCenter ?? throw new ArgumentNullException(nameof(messagingCenter));

            this.messagingCenter.Subscribe<JournalEntryDetails>(this, "LogAgainClicked", this.Refresh);
        }

        public decimal InsulinAmount
        {
            get { return this.insulinAmount; }
            set { this.SetProperty(ref this.insulinAmount, value); }
        }

        private void Refresh(JournalEntryDetails details)
        {
            this.InsulinAmount = details.InsulinAmount;
        }
    }
}
