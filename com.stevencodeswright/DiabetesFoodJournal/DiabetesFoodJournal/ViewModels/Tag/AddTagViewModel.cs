using DiabetesFoodJournal.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
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
        private readonly ITagService tagService;
        private string placeHolderText;

        public AddTagViewModel(IMessagingCenter messagingCenter, ITagService tagService)
        {
            this.messagingCenter = messagingCenter;
            this.tagService = tagService;
            this.height = 300;
            this.placeHolderText = "Tag";
            this.messagingCenter.Subscribe<JournalEntrySummary>(this, "JournalEntrySummarySelected", async (model) => await this.LoadTags(model));
        }

        public ObservableRangeCollection<string> InputItems { get; }

        public bool IsFocused
        {
            get { return this.isFocused; }
            set
            {
                if (this.SetProperty(ref this.isFocused, value))
                {
                    Height = value ? 300 : 300;
                }
            }
        }

        public int Height
        {
            get { return this.height; }
            set { this.SetProperty(ref this.height, value); }
        }

        public string PlaceHolderText
        {
            get { return this.placeHolderText; }
            set { this.SetProperty(ref this.placeHolderText, value); }
        }

        private object selectedItem;
        private bool isFocused;
        private int height;

        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                this.SetProperty(ref this.selectedItem, value);
            }
        }

        private async Task LoadTags(JournalEntrySummary model)
        {
            this.SelectedItem = await this.tagService.GetTagsForJournalEntryId(model.ID).ConfigureAwait(true);
        }
    }
}
