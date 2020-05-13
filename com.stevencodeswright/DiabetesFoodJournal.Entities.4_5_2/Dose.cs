using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    [Table(nameof(Dose), Schema = "FoodJournal")]
    public class Dose
    {
        [Key]
        public int Id { get; set; }
        public decimal InsulinAmount { get; set; }
        public int UpFront { get; set; }
        public int Extended { get; set; }
        public decimal TimeExtended { get; set; }
        public int TimeOffset { get; set; }
    }
}
