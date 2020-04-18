using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DiabetesFoodJournal.Models
{
    public class FoodSearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
