using System;
using System.Collections.Generic;

namespace DataLayer.Data
{
    public partial class Journalentrydose
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int DoseId { get; set; }
    }
}
