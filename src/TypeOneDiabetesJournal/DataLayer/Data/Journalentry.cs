using System;
using System.Collections.Generic;

namespace DataLayer.Data
{
    public partial class Journalentry
    {
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string? Notes { get; set; }
        public string? Title { get; set; }

        public virtual Journalentrynutritionalinfo? JournalEntryNutritionalInfo { get; set; }

        public virtual ICollection<Journalentrytag>? JournalEntryTags { get; set; }

    }
}
