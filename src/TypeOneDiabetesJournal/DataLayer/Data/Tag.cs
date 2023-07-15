using System;
using System.Collections.Generic;

namespace DataLayer.Data
{
    public partial class Tag
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Journalentrytag>? JournalEntryTags { get; set; }
    }
}
