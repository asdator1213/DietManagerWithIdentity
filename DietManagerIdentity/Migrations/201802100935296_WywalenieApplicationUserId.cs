namespace DietManagerIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WywalenieApplicationUserId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Dietician", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dietician", "ApplicationUserId", c => c.Int(nullable: false));
        }
    }
}
