using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Models
{
    public class GlucoseReading
    {
        public int Id { get; set; }
        public float Reading { get; set; }
        public DateTime DisplayTime { get; set; }
    }
}
