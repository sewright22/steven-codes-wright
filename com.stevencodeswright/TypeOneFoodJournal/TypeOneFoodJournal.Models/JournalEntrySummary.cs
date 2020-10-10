using System;
using System.Collections.Generic;
using System.Text;

namespace TypeOneFoodJournal.Models
{
    public class JournalEntrySummary
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int? CarbCount { get; set; }
        public DateTime? DateLogged { get; set; }
        public string Group { get; set; }
        public bool IsSelected { get; set; }
    }
}
