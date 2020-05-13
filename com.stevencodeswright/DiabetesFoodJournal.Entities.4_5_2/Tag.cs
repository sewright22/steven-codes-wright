using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
