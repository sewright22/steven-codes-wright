namespace DiabetesFoodJournal.Data.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFoodJournal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FoodJournal.Dose",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InsulinAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpFront = c.Int(nullable: false),
                        Extended = c.Int(nullable: false),
                        TimeExtended = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeOffset = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "FoodJournal.JournalEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logged = c.DateTime(nullable: false, precision: 0),
                        Notes = c.String(unicode: false),
                        Title = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "FoodJournal.JournalEntryDose",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JournalEntryId = c.Int(nullable: false),
                        DoseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "FoodJournal.JournalEntryNutritionalInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JournalEntryId = c.Int(nullable: false),
                        JournalEntryNutritionalInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "FoodJournal.JournalEntryTag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JournalEntryId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "FoodJournal.NutritionalInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Calories = c.Int(nullable: false),
                        Protein = c.Int(nullable: false),
                        Carbohydrates = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "FoodJournal.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("FoodJournal.Tag");
            DropTable("FoodJournal.NutritionalInfo");
            DropTable("FoodJournal.JournalEntryTag");
            DropTable("FoodJournal.JournalEntryNutritionalInfo");
            DropTable("FoodJournal.JournalEntryDose");
            DropTable("FoodJournal.JournalEntry");
            DropTable("FoodJournal.Dose");
        }
    }
}
