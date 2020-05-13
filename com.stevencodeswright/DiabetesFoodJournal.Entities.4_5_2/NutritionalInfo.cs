using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    [Table(nameof(NutritionalInfo), Schema = "FoodJournal")]
    public class NutritionalInfo
    {
        [Key]
        public int Id { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbohydrates { get; set; }
    }
}
