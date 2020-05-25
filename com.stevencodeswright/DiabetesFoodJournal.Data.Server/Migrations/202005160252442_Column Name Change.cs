namespace DiabetesFoodJournal.Data.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNameChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("FoodJournal.JournalEntryNutritionalInfo", "NutritionalInfoId", c => c.Int(nullable: false));
            DropColumn("FoodJournal.JournalEntryNutritionalInfo", "JournalEntryNutritionalInfoId");
        }
        
        public override void Down()
        {
            AddColumn("FoodJournal.JournalEntryNutritionalInfo", "JournalEntryNutritionalInfoId", c => c.Int(nullable: false));
            DropColumn("FoodJournal.JournalEntryNutritionalInfo", "NutritionalInfoId");
        }
    }
}
