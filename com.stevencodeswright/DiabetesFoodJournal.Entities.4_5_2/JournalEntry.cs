using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    public class JournalEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public IEnumerable<JournalEntryTag> JournalEntryTags { get; set; }
    }
}
