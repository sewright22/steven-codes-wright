namespace DiabetesFoodJournal.Data.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedschems : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "FoodJournal.Dose", newName: "Doses");
            RenameTable(name: "foodjournal.JournalEntry", newName: "JournalEntries");
            RenameTable(name: "FoodJournal.JournalEntryDose", newName: "JournalEntryDoses");
            RenameTable(name: "FoodJournal.JournalEntryNutritionalInfo", newName: "JournalEntryNutritionalInfoes");
            RenameTable(name: "FoodJournal.JournalEntryTag", newName: "JournalEntryTags");
            RenameTable(name: "FoodJournal.NutritionalInfo", newName: "NutritionalInfoes");
            RenameTable(name: "FoodJournal.Tag", newName: "Tags");
        }
        
        public override void Down()
        {
            RenameTable(name: "FoodJournal.Tags", newName: "Tag");
            RenameTable(name: "FoodJournal.NutritionalInfoes", newName: "NutritionalInfo");
            RenameTable(name: "FoodJournal.JournalEntryTags", newName: "JournalEntryTag");
            RenameTable(name: "FoodJournal.JournalEntryNutritionalInfoes", newName: "JournalEntryNutritionalInfo");
            RenameTable(name: "FoodJournal.JournalEntryDoses", newName: "JournalEntryDose");
            RenameTable(name: "foodjournal.JournalEntries", newName: "JournalEntry");
            RenameTable(name: "FoodJournal.Doses", newName: "Dose");
        }
    }
}
