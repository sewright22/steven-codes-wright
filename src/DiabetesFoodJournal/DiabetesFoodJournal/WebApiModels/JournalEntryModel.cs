using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiabetesFoodJournal.WebApiModels
{
    public class JournalEntryWebApiModel
    {
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public DoseWebApiModel Dose { get; set; } 
        public NutritionalInfoWebApiModel NutritionalInfo { get; set; }
        public List<TagWebApiModel> Tags { get; set; }
    }
}