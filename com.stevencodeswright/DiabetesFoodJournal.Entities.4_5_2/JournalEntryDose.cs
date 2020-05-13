using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    [Table(nameof(JournalEntryDose), Schema = "FoodJournal")]
    public class JournalEntryDose
    {
        [Key]
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int DoseId { get; set; }
    }
}
