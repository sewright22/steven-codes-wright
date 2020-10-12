using DiabetesFoodJournal.Services;
using GalaSoft.MvvmLight.Command;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels.JournalEntry
{
    public class JournalEntryViewModel : BaseViewModel
    {
        private readonly IMessagingCenter messagingCenter;

        public JournalEntryViewModel(IMessagingCenter messagingCenter)
        {
            this.messagingCenter = messagingCenter ?? throw new ArgumentNullException(nameof(messagingCenter));
            this.LogAgainCommand = new RelayCommand(this.LogAgainClicked);
        }

        private void LogAgainClicked()
        {
            this.messagingCenter.Send(this, "LogAgainClicked");
        }

        public RelayCommand LogAgainCommand { get; }
    }
}
