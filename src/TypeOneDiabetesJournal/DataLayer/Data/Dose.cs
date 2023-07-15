using System;
using System.Collections.Generic;

namespace DataLayer.Data
{
    public partial class Dose
    {
        public int Id { get; set; }
        public decimal InsulinAmount { get; set; }
        public int UpFront { get; set; }
        public int Extended { get; set; }
        public decimal TimeExtended { get; set; }
        public int TimeOffset { get; set; }
    }
}
