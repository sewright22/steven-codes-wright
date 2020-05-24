namespace TypeOneFoodJournal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class JournalEntryNutritionalInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int NutritionalInfoId { get; set; }
        public virtual JournalEntry JournalEntry { get; set; }
        public virtual NutritionalInfo NutritionalInfo { get; set; }
    }
}
