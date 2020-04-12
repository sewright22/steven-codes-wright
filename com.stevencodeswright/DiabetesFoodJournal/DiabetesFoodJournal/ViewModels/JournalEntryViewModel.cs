﻿using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalEntryViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IDataStore<Tag> tags;
        private string entryTitle;
        private int? carbCount;
        private decimal? timeExtended;
        private int? amountUpFront;
        private int? amountExtended;
        private decimal mealTimeOffset;
        private JournalEntryDataModel model;
        private string tagSearchText;

        public JournalEntryViewModel(IMessenger messenger, IDataStore<Tag> tags)
        {
            this.messenger = messenger;
            this.tags = tags;
            this.carbCount = 5;
            ExistingTagSearch = new ObservableRangeCollection<Tag>();
            if(this.messenger != null)
            {
                this.messenger.Register<JournalEntryDataModel>(this, JournalEntryReceived);
            }

            TagTappedCommand = new RelayCommand<Tag>(SearchTagTapped);
            this.PropertyChanged += this.JournalEntryViewModel_PropertyChanged;
        }

        private void SearchTagTapped(Tag tappedTag)
        {
            var tagDataModel = new TagDataModel();
            tagDataModel.Load(tappedTag);

            Model.Tags.Add(tagDataModel);
        }

        private async void JournalEntryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(AmountUpFront)))
            {
                if (AmountUpFront.HasValue)
                {
                    AmountExtended = 100 - AmountUpFront.Value;
                }
            }
            else if(e.PropertyName.Equals(nameof(AmountExtended)))
            {
                if (AmountExtended.HasValue)
                {
                    AmountUpFront = 100 - AmountExtended.Value;
                }
            }
            else if(e.PropertyName.Equals(nameof(TagSearchText)))
            {
                if(this.tagSearchText.Length>0)
                {
                    var results = from t in await tags.GetItemsAsync().ConfigureAwait(true)
                                  where t.Description.ToUpper().Contains(this.tagSearchText.ToUpper())
                                  select t;
                                 

                    ExistingTagSearch.ReplaceRange(results.Take(10));
                }
            }
        }

        public ObservableRangeCollection<Tag> ExistingTagSearch { get; }
        public NutritionalInfoDataModel NutritionalInfo { get; }
        public DoseDataModel Dose { get; }
        private void JournalEntryReceived(JournalEntryDataModel obj)
        {
            Model = obj;
        }

        public RelayCommand<Tag> TagTappedCommand { get; }

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

                SetProperty(ref this.mealTimeOffset, newStep*5);
            }
        }
    }
}
