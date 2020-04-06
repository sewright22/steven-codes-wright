using DiabetesFoodJournal.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels
{
    public class JournalViewModel : BaseViewModel
    {
        private readonly INavigationHelper navigation;
        private readonly IMessenger messenger;

        public JournalViewModel(INavigationHelper navigation, IMessenger messenger)
        {
            LocalSearchResults = new ObservableRangeCollection<FoodSearchResult>();
            SearchCommand = new RelayCommand<string>(async x => await SearchClicked(x).ConfigureAwait(true));
            ItemTappedCommand = new RelayCommand<FoodSearchResult>(async x => await ItemTapped(x).ConfigureAwait(true));
            this.navigation = navigation;
            this.messenger = messenger;
        }

        private async Task ItemTapped(FoodSearchResult foodResult)
        {
           await this.navigation.GoToAsync($"journalEntry").ConfigureAwait(false);
            this.messenger.Send(foodResult);
        }

        public ObservableRangeCollection<FoodSearchResult> LocalSearchResults { get; }

        public RelayCommand<string> SearchCommand { get; set; }
        public RelayCommand<FoodSearchResult> ItemTappedCommand { get; set; }

        private async Task SearchClicked(string x)
        {
            var foodList = new List<FoodSearchResult>();

            for(int i=1; i<=10;i++)
            {
                var foodItem = new FoodSearchResult()
                {
                    Id = i,
                    Name = $"{x} {i}"
                };

                foodItem.Tags.Add("DiGiorno");
                foodItem.Tags.Add("Thin Crust");

                await Task.Run(()=>foodList.Add(foodItem)).ConfigureAwait(true);
            }

            LocalSearchResults.AddRange(foodList);
        }
    }
}
