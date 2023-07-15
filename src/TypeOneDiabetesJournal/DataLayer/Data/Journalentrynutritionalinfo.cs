using System;
using System.Collections.Generic;

namespace DataLayer.Data
{
    public partial class Journalentrynutritionalinfo
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int NutritionalInfoId { get; set; }

        public virtual Nutritionalinfo? Nutritionalinfo { get; set; }
        public virtual Journalentry? JournalEntry { get; set; }
    }
}
