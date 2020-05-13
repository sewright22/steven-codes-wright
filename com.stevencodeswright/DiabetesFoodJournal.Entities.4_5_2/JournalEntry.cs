using System;
using System.ComponentModel.DataAnnotations;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    public class JournalEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
    }
}
