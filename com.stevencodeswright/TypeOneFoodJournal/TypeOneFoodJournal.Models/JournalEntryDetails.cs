using System;
using System.Collections.Generic;
using System.Text;

namespace TypeOneFoodJournal.Models
{
    public class JournalEntryDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Notes { get; set; }
        public decimal InsulinAmount { get; set; }
        public decimal TimeExtended { get; set; }
        public int UpFrontPercent { get; set; }
        public decimal UpFrontAmount { get; set; }
        public int ExtendedPercent { get; set; }
        public decimal ExtendedAmount { get; set; }
        public int TimeOffset { get; set; }
        public DateTime Logged { get; set; }
        public int TimeExtendedHours { get; set; }
        public int TimeExtendedMinutes { get; set; }
    }
}
