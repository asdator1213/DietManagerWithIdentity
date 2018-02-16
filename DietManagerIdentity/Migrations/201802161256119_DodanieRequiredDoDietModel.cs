namespace DietManagerIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanieRequiredDoDietModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Diet", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Diet", "Name", c => c.String());
        }
    }
}
