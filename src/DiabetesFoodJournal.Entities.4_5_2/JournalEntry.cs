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
        public virtual ICollection<JournalEntryTag> JournalEntryTags { get; set; }
        public virtual ICollection<JournalEntryDose> JournalEntryDoses { get; set; }
        public virtual ICollection<JournalEntryNutritionalInfo> JournalEntryNutritionalInfos { get; set; }
    }
}
