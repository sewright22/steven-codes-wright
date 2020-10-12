using System;
using System.Collections.Generic;
using System.Text;

namespace TypeOneFoodJournal.Models
{
    public class AdvancedBloodSugarStats
    {
        public float? HighestReading { get; set; }
        public float? LowestReading { get; set; }
        public int? HighestReadingTime { get; set; }
        public int? LowestReadingTime { get; set; }
        public float? StartingBloodSugar { get; set; }
    }
}
