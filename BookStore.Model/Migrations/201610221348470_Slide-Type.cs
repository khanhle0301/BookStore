namespace BookStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SlideType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "GroupID", "dbo.UserGroups");
            DropIndex("dbo.Users", new[] { "GroupID" });
            AddColumn("dbo.Slides", "Type", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "GroupID", c => c.Int());
            CreateIndex("dbo.Users", "GroupID");
            AddForeignKey("dbo.Users", "GroupID", "dbo.UserGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "GroupID", "dbo.UserGroups");
            DropIndex("dbo.Users", new[] { "GroupID" });
            AlterColumn("dbo.Users", "GroupID", c => c.Int(nullable: false));
            DropColumn("dbo.Slides", "Type");
            CreateIndex("dbo.Users", "GroupID");
            AddForeignKey("dbo.Users", "GroupID", "dbo.UserGroups", "ID", cascadeDelete: true);
        }
    }
}
