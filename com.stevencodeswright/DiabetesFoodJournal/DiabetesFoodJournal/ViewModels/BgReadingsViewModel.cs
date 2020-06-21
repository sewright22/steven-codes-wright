using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.DataServices;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.ViewModels
{
    public class BgReadingsViewModel : BaseViewModel
    {
        private readonly IBgReadingsDataService dataService;
        private readonly IMessenger messenger;
        private JournalEntryDataModel model;

        public BgReadingsViewModel(IBgReadingsDataService dataService, IMessenger messenger)
        {
            this.dataService = dataService;
            this.messenger = messenger;

            if (this.messenger != null)
            {
                this.messenger.Register<JournalEntryDataModel>(this, async (model) => await JournalEntryReceived(model));
            }
        }

        public JournalEntryDataModel Model
        {
            get
            {
                return this.model;
            }
            set
            {
                SetProperty(ref this.model, value);
            }
        }

        private async Task JournalEntryReceived(JournalEntryDataModel model)
        {
            this.Model = model;

            if(this.Model.BgReadings.Any() == false)
            {
                var readingsFromCgm = await this.dataService.GetCgmReadings(this.model.Logged);

                if(readingsFromCgm.Any())
                {
                    this.Model.BgReadings.AddRange(readingsFromCgm);
                }
            }
        }
    }
}
