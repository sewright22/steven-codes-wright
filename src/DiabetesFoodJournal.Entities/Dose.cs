using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Entities
{
    public class Dose
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal InsulinAmount { get; set; }
        public int UpFront { get; set; }
        public int Extended { get; set; }
        public decimal TimeExtended { get; set; }
        public int TimeOffset { get; set; }
    }
}
