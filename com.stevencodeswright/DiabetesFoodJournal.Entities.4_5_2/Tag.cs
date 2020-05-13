using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    [Table(nameof(Tag), Schema = "FoodJournal")]
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
