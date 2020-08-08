using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Models
{
    public class ChartReading : GlucoseReading
    {
        public string FoodName { get; set; }
        public string InsulinAmount { get; set; }
    }
}
