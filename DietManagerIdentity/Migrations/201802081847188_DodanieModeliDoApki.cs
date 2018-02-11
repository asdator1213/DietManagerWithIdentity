namespace DietManagerIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanieModeliDoApki : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dietician",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        DateOfAddition = c.DateTime(nullable: false),
                        DietLength = c.Int(nullable: false),
                        NumberOfConsultation = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        PatientAge = c.Int(nullable: false),
                        Dislikes = c.String(nullable: false),
                        Allergy = c.String(nullable: false),
                        PlannedWeight = c.Int(nullable: false),
                        Contraindications = c.String(nullable: false),
                        DietId = c.Int(),
                        DieticianId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diet", t => t.DietId)
                .ForeignKey("dbo.Dietician", t => t.DieticianId, cascadeDelete: true)
                .Index(t => t.DietId)
                .Index(t => t.DieticianId);
            
            CreateTable(
                "dbo.Diet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateOfAddition = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateOfAddition = c.DateTime(nullable: false),
                        MealType = c.Int(nullable: false),
                        ShoppingList = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeightData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Weight = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patient", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.MealDiet",
                c => new
                    {
                        DietId = c.Int(nullable: false),
                        MealId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DietId, t.MealId })
                .ForeignKey("dbo.Meal", t => t.DietId, cascadeDelete: true)
                .ForeignKey("dbo.Diet", t => t.MealId, cascadeDelete: true)
                .Index(t => t.DietId)
                .Index(t => t.MealId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeightData", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Patient", "DieticianId", "dbo.Dietician");
            DropForeignKey("dbo.Patient", "DietId", "dbo.Diet");
            DropForeignKey("dbo.MealDiet", "MealId", "dbo.Diet");
            DropForeignKey("dbo.MealDiet", "DietId", "dbo.Meal");
            DropForeignKey("dbo.Dietician", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.MealDiet", new[] { "MealId" });
            DropIndex("dbo.MealDiet", new[] { "DietId" });
            DropIndex("dbo.WeightData", new[] { "PatientId" });
            DropIndex("dbo.Patient", new[] { "DieticianId" });
            DropIndex("dbo.Patient", new[] { "DietId" });
            DropIndex("dbo.Dietician", new[] { "UserId" });
            DropTable("dbo.MealDiet");
            DropTable("dbo.WeightData");
            DropTable("dbo.Meal");
            DropTable("dbo.Diet");
            DropTable("dbo.Patient");
            DropTable("dbo.Dietician");
        }
    }
}
