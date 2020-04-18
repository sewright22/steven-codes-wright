using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Models
{
    public class Dose
    {
        public int Id { get; set; }

        public int InsulinAmount { get; set; }
        public int UpFront { get; set; }
        public int Extended { get; set; }
        public decimal TimeExtended { get; set; }
        public int TimeOffset { get; set; }
    }
}
