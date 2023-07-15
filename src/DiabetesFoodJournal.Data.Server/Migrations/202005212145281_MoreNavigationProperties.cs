namespace DiabetesFoodJournal.Data.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreNavigationProperties : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.JournalEntryDoses", "JournalEntryId", false, "IX_DoseJournalEntryId");
            CreateIndex("dbo.JournalEntryDoses", "DoseId", false, "IX_JournalEntryDoseId");
            CreateIndex("dbo.JournalEntryNutritionalInfoes", "JournalEntryId", false, "IX_NutritionalInfoJournalEntryId");
            CreateIndex("dbo.JournalEntryNutritionalInfoes", "NutritionalInfoId", false, "IX_JournalEntryNutritionalInfoId");
            AddForeignKey("dbo.JournalEntryDoses", "DoseId", "dbo.Doses", "Id", cascadeDelete: true, "JournalEntryDoses_DoseId");
            AddForeignKey("dbo.JournalEntryDoses", "JournalEntryId", "dbo.JournalEntries", "Id", cascadeDelete: true, "JournalEntryDoses_JournalEntryDoseId");
            AddForeignKey("dbo.JournalEntryNutritionalInfoes", "JournalEntryId", "dbo.JournalEntries", "Id", cascadeDelete: true, "JournalEntryNutritionalInfoes_JournalEntryId");
            AddForeignKey("dbo.JournalEntryNutritionalInfoes", "NutritionalInfoId", "dbo.NutritionalInfoes", "Id", cascadeDelete: true, "JournalEntryNutritionalInfoes_NutritionalInfoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalEntryNutritionalInfoes", "NutritionalInfoId", "dbo.NutritionalInfoes");
            DropForeignKey("dbo.JournalEntryNutritionalInfoes", "JournalEntryId", "dbo.JournalEntries");
            DropForeignKey("dbo.JournalEntryDoses", "JournalEntryId", "dbo.JournalEntries");
            DropForeignKey("dbo.JournalEntryDoses", "DoseId", "dbo.Doses");
            DropIndex("dbo.JournalEntryNutritionalInfoes", new[] { "JournalEntryNutritionalInfoId" });
            DropIndex("dbo.JournalEntryNutritionalInfoes", new[] { "NutritionalInfoJournalEntryId" });
            DropIndex("dbo.JournalEntryDoses", new[] { "JournalEntryDoseId" });
            DropIndex("dbo.JournalEntryDoses", new[] { "DoseJournalEntryId" });
        }
    }
}
