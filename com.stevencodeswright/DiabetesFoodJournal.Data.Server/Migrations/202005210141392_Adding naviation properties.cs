namespace DiabetesFoodJournal.Data.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingnaviationproperties : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.JournalEntryTags", "JournalEntryId");
            CreateIndex("dbo.JournalEntryTags", "TagId");
            AddForeignKey("dbo.JournalEntryTags", "JournalEntryId", "dbo.JournalEntries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JournalEntryTags", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalEntryTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.JournalEntryTags", "JournalEntryId", "dbo.JournalEntries");
            DropIndex("dbo.JournalEntryTags", new[] { "TagId" });
            DropIndex("dbo.JournalEntryTags", new[] { "JournalEntryId" });
        }
    }
}
