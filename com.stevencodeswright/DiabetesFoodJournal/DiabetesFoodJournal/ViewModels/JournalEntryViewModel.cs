using DiabetesFoodJournal.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalEntryViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private string food;

        public JournalEntryViewModel(IMessenger messenger)
        {
            this.messenger = messenger;

            if(this.messenger != null)
            {
                this.messenger.Register<FoodSearchResult>(this, FoodReceived);
            }
        }

        private void FoodReceived(FoodSearchResult obj)
        {
            Food = obj.Name;
        }

        public string Food
        {
            get
            {
                return this.food;
            }
            set
            {
                SetProperty(ref this.food, value);
            }
        }
    }
}
