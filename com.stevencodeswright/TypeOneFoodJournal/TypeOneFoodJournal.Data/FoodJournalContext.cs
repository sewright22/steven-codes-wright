namespace TypeOneFoodJournal.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TypeOneFoodJournal.Entities;

    public class FoodJournalContext : DbContext
    {
        public FoodJournalContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<Dose> Doses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NutritionalInfo> NutritionalInfos { get; set; }
        public DbSet<JournalEntryTag> JournalEntryTags { get; set; }
        public DbSet<JournalEntryNutritionalInfo> JournalEntryNutritionalInfos { get; set; }
        public DbSet<JournalEntryDose> JournalEntryDoses { get; set; }
    }
}
