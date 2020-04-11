using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Models;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalEntryViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private string entryTitle;
        private int? carbCount;
        private decimal? timeExtended;
        private int? amountUpFront;
        private int? amountExtended;
        private decimal mealTimeOffset;

        public JournalEntryViewModel(IMessenger messenger)
        {
            this.messenger = messenger;
            this.carbCount = 5;
            Tags = new ObservableRangeCollection<TagDataModel>();
            if(this.messenger != null)
            {
                this.messenger.Register<FoodSearchResult>(this, FoodReceived);
            }

            this.PropertyChanged += this.JournalEntryViewModel_PropertyChanged;
        }

        private void JournalEntryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
        }

        public ObservableRangeCollection<TagDataModel> Tags { get; }
        public NutritionalInfoDataModel NutritionalInfo { get; }
        public DoseDataModel Dose { get; }
        private void FoodReceived(FoodSearchResult obj)
        {
            EntryTitle = obj.Name;
            var tagList = new List<TagDataModel>();
            //tagList.Add("Pizza Hut");
            //tagList.Add("Pan");
            //tagList.Add("Stuffed Crust");
            //tagList.Add("Extra Cheese");
            //tagList.Add("Meat Lovers");
            //tagList.Add("Deep Dish");
            Device.BeginInvokeOnMainThread(() => Tags.ReplaceRange(tagList));
        }

        public string EntryTitle
        {
            get
            {
                return this.entryTitle;
            }
            set
            {
                SetProperty(ref this.entryTitle, value);
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
