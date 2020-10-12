using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalEntrysViewModel : BaseViewModel
    {
        private readonly IJournalEntryDataService dataService;
        private readonly IMessenger messenger;
        private readonly INavigationHelper navigation;
        private readonly IDataStore<Tag> tags;
        private int? carbCount;
        private decimal? timeExtended;
        private int? amountUpFront;
        private int? amountExtended;
        private decimal mealTimeOffset;
        private JournalEntryDataModel model;
        private string tagSearchText;

        public JournalEntrysViewModel(IJournalEntryDataService dataService, IMessenger messenger, INavigationHelper navigation, IDataStore<Tag> tags)
        {
            this.dataService = dataService;
            this.messenger = messenger;
            this.navigation = navigation;
            this.tags = tags;
            this.carbCount = 5;
            ExistingTagSearch = new ObservableRangeCollection<Tag>();

            if (this.messenger != null)
            {
                this.messenger.Register<JournalEntryDataModel>(this, JournalEntryReceived);
            }

            ConfirmDeleteTappedCommand = new RelayCommand(ConfirmDeleteTapped);
            ExistingTagTappedCommand = new RelayCommand<TagDataModel>(ExistingTagTapped);
            CreateNewTagCommand = new RelayCommand<string>(async (x) => await CreateNewTag(x));
            TagTappedCommand = new RelayCommand<Tag>(SearchTagTapped);
            SaveCommand = new RelayCommand(async () => await SaveEntry());
            this.PropertyChanged += this.JournalEntryViewModel_PropertyChanged;
        }

        private async Task SaveEntry()
        {
            await this.dataService.SaveEntry(Model).ConfigureAwait(false);
            await this.navigation.GoToAsync("..").ConfigureAwait(false);
        }

        private async Task CreateNewTag(string newTag)
        {
            if (ExistingTagSearch.FirstOrDefault(x => x.Description.Equals(newTag, StringComparison.OrdinalIgnoreCase)) == null)
            {
                var tagId = await this.dataService.AddNewTag(new Tag
                {
                    Description = newTag
                });

                if (tagId > 0)
                {
                    JournalEntryViewModel_PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(TagSearchText)));
                }
            }
        }

        private void ConfirmDeleteTapped()
        {
            var selectedTag = Model.Tags.FirstOrDefault(x => x.CanDelete);

            if (selectedTag != null)
            {
                Model.Tags.Remove(selectedTag);
            }
        }

        private void ExistingTagTapped(TagDataModel tappedTag)
        {
            var selectedTag = Model.Tags.FirstOrDefault(x => x.CanDelete);

            if (selectedTag != null)
            {
                selectedTag.CanDelete = false;
            }

            tappedTag.CanDelete = true;
        }

        private void SearchTagTapped(Tag tappedTag)
        {
            var tagDataModel = new TagDataModel();
            tagDataModel.Load(tappedTag);

            Model.Tags.Add(tagDataModel);
            this.TagSearchText = "";
        }

        private async void JournalEntryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(TagSearchText)))
            {
                if (string.IsNullOrWhiteSpace(this.TagSearchText) == false)
                {
                    var results = await this.dataService.GetTags(TagSearchText);


                    ExistingTagSearch.ReplaceRange(results);
                }
            }
        }

        public ObservableRangeCollection<Tag> ExistingTagSearch { get; }
        private void JournalEntryReceived(JournalEntryDataModel obj)
        {
            if (Model != null && Model.Dose != null)
            {
                Model.Dose.PropertyChanged -= this.Dose_PropertyChanged;
            }
            Model = obj;
            TagSearchText = "";
            Model.Dose.PropertyChanged += this.Dose_PropertyChanged;
        }

        private void Dose_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Model.Dose.UpFront)))
            {
                Model.Dose.Extended = 100 - Model.Dose.UpFront;
            }
            else if (e.PropertyName.Equals(nameof(Model.Dose.Extended)))
            {
                Model.Dose.UpFront = 100 - Model.Dose.Extended;
            }
        }

        public RelayCommand<Tag> TagTappedCommand { get; }
        public RelayCommand<TagDataModel> ExistingTagTappedCommand { get; }
        public RelayCommand ConfirmDeleteTappedCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand<string> CreateNewTagCommand { get; }

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

        public string TagSearchText
        {
            get
            {
                return this.tagSearchText;
            }
            set
            {
                SetProperty(ref this.tagSearchText, value);
            }
        }

        public int? CarbCount
        {
            get
            {
                return this.carbCount;
            }
            set
            {
                SetProperty(ref this.carbCount, value);
            }
        }
        public int? AmountUpFront
        {
            get
            {
                return this.amountUpFront;
            }
            set
            {
                SetProperty(ref this.amountUpFront, value);
            }
        }
        public int? AmountExtended
        {
            get
            {
                return this.amountExtended;
            }
            set
            {
                SetProperty(ref this.amountExtended, value);
            }
        }
        public decimal? TimeExtended
        {
            get
            {
                return this.timeExtended;
            }
            set
            {
                SetProperty(ref this.timeExtended, value);
            }
        }
        public decimal MealTimeOffset
        {
            get
            {
                return this.mealTimeOffset;
            }
            set
            {
                var newStep = Math.Round(value / 5);

                SetProperty(ref this.mealTimeOffset, newStep * 5);
            }
        }
    }
}
