using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    [Table(nameof(JournalEntryTag), Schema = "FoodJournal")]
    public class JournalEntryTag
    {
        [Key]
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int TagId { get; set; }
    }
}
