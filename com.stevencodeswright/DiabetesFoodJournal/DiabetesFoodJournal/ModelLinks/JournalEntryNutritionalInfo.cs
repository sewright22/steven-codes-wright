using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.ModelLinks
{
    public class JournalEntryNutritionalInfo
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int JournalEntryNutritionalInfoId { get; set; }
    }
}
