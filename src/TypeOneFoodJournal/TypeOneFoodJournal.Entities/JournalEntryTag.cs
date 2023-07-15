namespace TypeOneFoodJournal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class JournalEntryTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int TagId { get; set; }
        public virtual JournalEntry JournalEntry { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
