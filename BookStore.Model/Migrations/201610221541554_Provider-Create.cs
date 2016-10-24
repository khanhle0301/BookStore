namespace BookStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProviderCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "ProviderID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProviderID");
            AddForeignKey("dbo.Products", "ProviderID", "dbo.Providers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProviderID", "dbo.Providers");
            DropIndex("dbo.Products", new[] { "ProviderID" });
            DropColumn("dbo.Products", "ProviderID");
            DropTable("dbo.Providers");
        }
    }
}
