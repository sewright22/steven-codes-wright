using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.ModelLinks
{
    public class JournalEntryDose
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int DoseId { get; set; }
    }
}
