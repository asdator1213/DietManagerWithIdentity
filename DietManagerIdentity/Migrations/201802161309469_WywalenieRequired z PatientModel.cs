namespace DietManagerIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WywalenieRequiredzPatientModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patient", "Dislikes", c => c.String());
            AlterColumn("dbo.Patient", "Allergy", c => c.String());
            AlterColumn("dbo.Patient", "Contraindications", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patient", "Contraindications", c => c.String(nullable: false));
            AlterColumn("dbo.Patient", "Allergy", c => c.String(nullable: false));
            AlterColumn("dbo.Patient", "Dislikes", c => c.String(nullable: false));
        }
    }
}
