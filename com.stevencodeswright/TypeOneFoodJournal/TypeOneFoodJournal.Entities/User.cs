using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TypeOneFoodJournal.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public virtual UserPassword UserPassword { get; set; }
        public virtual ICollection<UserJournalEntry> UserJournalEntries { get; set; }
    }
}
