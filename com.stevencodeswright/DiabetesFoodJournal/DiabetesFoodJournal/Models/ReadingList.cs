using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Models
{
    public partial class ReadingList
    {
        public string Unit { get; set; }
        public string RateUnit { get; set; }
        public List<Egv> Egvs { get; set; }
    }

    public partial class Egv
    {
        public DateTimeOffset SystemTime { get; set; }
        public DateTimeOffset DisplayTime { get; set; }
        public long Value { get; set; }
        public long RealtimeValue { get; set; }
        public long SmoothedValue { get; set; }
        public object Status { get; set; }
        public Trend Trend { get; set; }
        public double? TrendRate { get; set; }
    }

    public enum Trend { DoubleDown, Flat, FortyFiveDown, FortyFiveUp, NotComputable, SingleDown, SingleUp };
}

