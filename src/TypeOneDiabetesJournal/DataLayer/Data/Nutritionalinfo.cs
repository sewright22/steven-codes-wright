using System;
using System.Collections.Generic;

namespace DataLayer.Data
{
    public partial class Nutritionalinfo
    {
        public int Id { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbohydrates { get; set; }

        public virtual Journalentrynutritionalinfo? JournalEntryNutritionalInfo { get; set; }
    }
}
