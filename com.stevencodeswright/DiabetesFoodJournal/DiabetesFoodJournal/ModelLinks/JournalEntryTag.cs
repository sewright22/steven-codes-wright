using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.ModelLinks
{
    public class JournalEntryTag
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int TagId { get; set; }
    }
}
