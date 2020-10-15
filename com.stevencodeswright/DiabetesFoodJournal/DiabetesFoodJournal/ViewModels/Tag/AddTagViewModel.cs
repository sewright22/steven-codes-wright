using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels.Tag
{
    public class AddTagViewModel : BaseViewModel
    {
        private readonly IMessagingCenter messagingCenter;
        private string placeHolderText;

        public AddTagViewModel(IMessagingCenter messagingCenter)
        {
            this.InputItems = new ObservableRangeCollection<string>() { "Washer", "Television", "Air Conditioner" };
            this.selectedItem = new List<string>() { "Washer" };
            this.messagingCenter = messagingCenter;
            this.placeHolderText = "Tag";
            //this.messagingCenter.Subscribe<JournalEntrySummary>(this, "JournalEntrySummarySelected", async (model) => await this.LoadTags(model));
        }

        public ObservableRangeCollection<string> InputItems { get; }

        public string PlaceHolderText
        {
            get { return this.placeHolderText; }
            set { this.SetProperty(ref this.placeHolderText, value); }
        }

        private object selectedItem;

        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                this.SetProperty(ref this.selectedItem, value);
            }
        }

        private Task LoadTags(JournalEntrySummary model)
        {
            var test = new List<string>();
            test.Add("Item 1");
            test.Add("Item 2");
            test.Add("Item 3");

            this.InputItems.ReplaceRange(test);
            return Task.Run(() => Thread.Sleep(100));
        }
    }
}
