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
        private string foodName;
        private int? carbCount;

        public JournalEntryViewModel(IMessenger messenger)
        {
            this.messenger = messenger;
            this.carbCount = 5;
            Tags = new ObservableRangeCollection<string>();
            if(this.messenger != null)
            {
                this.messenger.Register<FoodSearchResult>(this, FoodReceived);
            }
        }

        public ObservableRangeCollection<string> Tags { get; }

        private void FoodReceived(FoodSearchResult obj)
        {
            FoodName = obj.Name;
            var tagList = new List<string>();
            tagList.Add("Pizza Hut");
            tagList.Add("Pan");
            tagList.Add("Stuffed Crust");
            tagList.Add("Extra Cheese");
            tagList.Add("Meat Lovers");
            tagList.Add("Deep Dish");
            Device.BeginInvokeOnMainThread(() => Tags.ReplaceRange(tagList));
        }

        public string FoodName
        {
            get
            {
                return this.foodName;
            }
            set
            {
                SetProperty(ref this.foodName, value);
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
    }
}
