using System;
using System.Collections.Generic;

namespace DataLayer.Data
{
    public partial class Userjournalentry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JournalEntryId { get; set; }
    }
}
