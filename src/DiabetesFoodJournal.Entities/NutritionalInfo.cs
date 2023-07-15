using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Entities
{
    public class NutritionalInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbohydrates { get; set; }
    }
}
