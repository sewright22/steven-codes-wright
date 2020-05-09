using System;

namespace DiabetesFoodJournal.Entities
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
    }
}
