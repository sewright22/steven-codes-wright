using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Entities
{
    public class JournalEntryTag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int TagId { get; set; }
    }
}
