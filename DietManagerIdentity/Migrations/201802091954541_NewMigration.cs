namespace DietManagerIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Dietician", name: "UserId", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Dietician", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            AddColumn("dbo.Dietician", "ApplicationUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dietician", "ApplicationUserId");
            RenameIndex(table: "dbo.Dietician", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.Dietician", name: "ApplicationUser_Id", newName: "UserId");
        }
    }
}
