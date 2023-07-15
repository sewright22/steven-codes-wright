using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private string _label;

        public override async Task Initialize()
        {
            await base.Initialize();

            _label = "MvvmCross";

            ClickCommand = new MvxAsyncCommand(() => ButtonClicked());
            ItemList = new ObservableCollection<string>();
            ItemList.Add("Food 1");
            ItemList.Add("Food 2");
            ItemList.Add("Food 3");
            ItemList.Add("Food 4");
        }

        private Task ButtonClicked()
        {
            return Task.Run(()=> { Label = "Clicked"; });
        }

        public string Label
        {
            get => _label;
            set
            {
                _label = value;
                RaisePropertyChanged(() => Label);
            }
        }

        public IMvxAsyncCommand ClickCommand { get; private set; }
        public ObservableCollection<string> ItemList { get; private set; }
    }
}
