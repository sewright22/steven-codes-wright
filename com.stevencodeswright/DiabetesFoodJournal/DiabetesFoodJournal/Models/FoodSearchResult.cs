using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DiabetesFoodJournal.Models
{
    public class FoodSearchResult
    {
        public string Name { get; set; }
        public ObservableCollection<string> Tags { get; set; } = new ObservableCollection<string>();
    }
}
