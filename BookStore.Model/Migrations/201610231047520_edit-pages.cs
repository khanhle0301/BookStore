namespace BookStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editpages : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Pages");
            AlterColumn("dbo.Pages", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Pages", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Pages");
            AlterColumn("dbo.Pages", "ID", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Pages", "ID");
        }
    }
}
