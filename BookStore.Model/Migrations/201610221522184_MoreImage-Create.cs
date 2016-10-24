namespace BookStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreImageCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MoreImages", c => c.String(storeType: "xml"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "MoreImages");
        }
    }
}
