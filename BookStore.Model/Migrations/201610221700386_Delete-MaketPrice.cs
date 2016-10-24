namespace BookStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMaketPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PromotionPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Products", "OriginalPrice");
            DropColumn("dbo.Products", "MarketPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MarketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "PromotionPrice");
        }
    }
}
