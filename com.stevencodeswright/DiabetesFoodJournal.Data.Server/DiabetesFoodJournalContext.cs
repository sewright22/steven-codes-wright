namespace DiabetesFoodJournal.Data.Server
{
    using DiabetesFoodJournal.Entities._4_5_2;
    using MySql.Data.Entity;
    using System.Data.Entity;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DiabetesFoodJournalContext : DbContext
    {
        public DiabetesFoodJournalContext()
            : base("name=DiabetesFoodJournalEntity")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JournalEntry>();
            modelBuilder.Entity<Dose>();
            modelBuilder.Entity<Tag>();
            modelBuilder.Entity<NutritionalInfo>();
            modelBuilder.Entity<JournalEntryTag>();
            modelBuilder.Entity<JournalEntryNutritionalInfo>();
            modelBuilder.Entity<JournalEntryDose>();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<Dose> Doses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NutritionalInfo> NutritionalInfos { get; set; }
        public DbSet<JournalEntryTag> JournalEntryTags { get; set; }
        public DbSet<JournalEntryNutritionalInfo> JournalEntryNutritionalInfos { get; set; }
        public DbSet<JournalEntryDose> JournalEntryDoses { get; set; }
    }
}