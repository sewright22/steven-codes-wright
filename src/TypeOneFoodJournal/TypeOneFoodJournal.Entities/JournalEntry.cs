namespace TypeOneFoodJournal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class JournalEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public virtual ICollection<JournalEntryTag> JournalEntryTags { get; set; }
        public virtual ICollection<JournalEntryDose> JournalEntryDoses { get; set; }
        public virtual ICollection<JournalEntryNutritionalInfo> JournalEntryNutritionalInfos { get; set; }
    }
}
