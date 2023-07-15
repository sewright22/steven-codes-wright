using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Entities
{
    public class JournalEntryNutritionalInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int JournalEntryNutritionalInfoId { get; set; }
    }
}
