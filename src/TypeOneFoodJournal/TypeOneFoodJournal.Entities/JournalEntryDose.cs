

namespace TypeOneFoodJournal.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class JournalEntryDose
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int DoseId { get; set; }
        public virtual JournalEntry JournalEntry { get; set; }
        public virtual Dose Dose { get; set; }
    }
}
