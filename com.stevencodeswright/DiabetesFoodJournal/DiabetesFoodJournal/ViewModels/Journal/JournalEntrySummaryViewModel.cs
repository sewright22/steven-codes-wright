using DiabetesFoodJournal.Views.Journal;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels.Journal
{
    public class JournalEntrySummaryViewModel : BaseViewModel
    {
        private JournalEntrySummary model;
        private bool isSelected;
        private string group;
        private readonly INavigationHelper navigationHelper;
        private readonly IMessagingCenter messagingCenter;

        public JournalEntrySummaryViewModel(INavigationHelper navigationHelper, IMessagingCenter messagingCenter)
        {
            this.TappedCommand = new AsyncCommand(this.Select);
            this.ViewReadingsCommand = new AsyncCommand(this.GoToReadings);
            this.UpdateCommand = new AsyncCommand(this.Update);
            this.navigationHelper = navigationHelper;
            this.messagingCenter = messagingCenter;
        }

        public JournalEntrySummary Model
        {
            get { return this.model; }
            set
            {
                if (this.SetProperty(ref this.model, value))
                {
                    this.IsSelected = this.model.IsSelected;
                    this.Group = this.model.Group;
                }
            }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetProperty(ref this.isSelected, value); }
        }

        public string Group
        {
            get { return this.group; }
            set { this.SetProperty(ref this.group, value); }
        }

        private async Task Update()
        {
            try
            {
                await this.navigationHelper.GoToAsync("journalEntry/update").ConfigureAwait(true);
                this.messagingCenter.Send(this.Model, "EditExisting");
                this.messagingCenter.Send(this.Model, "JournalEntrySummarySelected");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private async Task GoToReadings()
        {
            try
            {
                await this.navigationHelper.GoToAsync("journalEntry/details").ConfigureAwait(true);
                this.messagingCenter.Send(this.Model, "LoadReadings");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private Task Select()
        {
            return Task.Run(() => { this.IsSelected = true; });
        }

        public AsyncCommand TappedCommand { get; set; }
        public AsyncCommand ViewReadingsCommand { get; set; }
        public AsyncCommand UpdateCommand { get; set; }
    }
}
