namespace BookStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPost : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Tags", c => c.String());
        }
    }
}
