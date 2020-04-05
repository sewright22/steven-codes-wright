using DiabetesFoodJournal.Models;
using GalaSoft.MvvmLight.Command;
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
        private readonly IDeviceHelper deviceHelper;

        public JournalViewModel(IDeviceHelper deviceHelper)
        {
            LocalSearchResults = new ObservableRangeCollection<FoodSearchResult>();
            SearchCommand = new RelayCommand<string>(async x => await SearchClicked(x).ConfigureAwait(true));
            this.deviceHelper = deviceHelper;
        }

        public ObservableRangeCollection<FoodSearchResult> LocalSearchResults { get; }

        public RelayCommand<string> SearchCommand { get; set; }

        private async Task SearchClicked(string x)
        {
            var foodList = new List<FoodSearchResult>();

            for(int i=1; i<=10;i++)
            {
                var foodItem = new FoodSearchResult()
                {
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
