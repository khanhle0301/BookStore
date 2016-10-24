namespace BookStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BannerCraete : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Image = c.String(maxLength: 256),
                        Url = c.String(maxLength: 256),
                        Type = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Slides", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slides", "Type", c => c.String(nullable: false, maxLength: 50));
            DropTable("dbo.Banners");
        }
    }
}
