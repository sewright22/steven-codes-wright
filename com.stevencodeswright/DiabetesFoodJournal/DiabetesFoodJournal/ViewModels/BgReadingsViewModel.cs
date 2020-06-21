using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.DataServices;
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

        public BgReadingsViewModel(IBgReadingsDataService dataService, IMessenger messenger, INavigationHelper navigation)
        {
            this.dataService = dataService;
            this.messenger = messenger;
            this.navigation = navigation;
            this.LogAgainCommand = new AsyncCommand(this.LogAgainClicked);

            if (this.messenger != null)
            {
                this.messenger.Register<JournalEntryDataModel>(this, async (model) => await JournalEntryReceived(model));
            }
        }

        private async Task LogAgainClicked()
        {
            await this.navigation.GoToAsync($"journalEntry").ConfigureAwait(false);
            var entry = this.dataService.Copy(this.Model);
            entry.Logged = DateTime.Now;

            entry = await this.dataService.SaveEntry(entry).ConfigureAwait(false);
            if (entry.Id > 0)
            {
                this.messenger.Send(entry);
            }
        }

        public JournalEntryDataModel Model { get { return this.model; } set { SetProperty(ref this.model, value); } }
        public AsyncCommand LogAgainCommand { get; set; }

        private async Task JournalEntryReceived(JournalEntryDataModel model)
        {
            this.Model = model;

            if (this.Model.BgReadings.Any() == false)
            {
                IsBusy = true;
                try
                {
                    var readingsFromCgm = await this.dataService.GetCgmReadings(this.model.Logged).ConfigureAwait(true);

                    if (readingsFromCgm.Any())
                    {

                        Device.BeginInvokeOnMainThread(() => this.Model.BgReadings.ReplaceRange(readingsFromCgm.OrderBy(x => x.DisplayTime)));
                    }
                }
                catch (Exception)
                {
                    Device.BeginInvokeOnMainThread(() => this.Model.BgReadings.Clear());
                }
            }

            IsBusy = false;
        }
    }
}
