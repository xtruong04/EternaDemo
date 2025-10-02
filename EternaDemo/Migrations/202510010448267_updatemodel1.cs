namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductVariants", "MetalId", "dbo.Metals");
            DropForeignKey("dbo.OrderItems", "ProductVariantId", "dbo.ProductVariants");
            DropForeignKey("dbo.ProductVariants", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "GemstoneId", "dbo.Gemstones");
            DropIndex("dbo.OrderItems", new[] { "ProductVariantId" });
            DropIndex("dbo.ProductVariants", new[] { "ProductId" });
            DropIndex("dbo.ProductVariants", new[] { "MetalId" });
            AddColumn("dbo.OrderItems", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "MetalId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderItems", "ProductId");
            CreateIndex("dbo.Products", "MetalId");
            AddForeignKey("dbo.Products", "MetalId", "dbo.Metals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Products", "GemstoneId", "dbo.Gemstones", "Id");
            DropColumn("dbo.OrderItems", "ProductVariantId");
            DropColumn("dbo.ProductImages", "IsPrimary");
            DropTable("dbo.ProductVariants");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        VariantSKU = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQuantity = c.Int(nullable: false),
                        MetalId = c.Int(nullable: false),
                        Size = c.String(maxLength: 20),
                        Color = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductImages", "IsPrimary", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderItems", "ProductVariantId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "GemstoneId", "dbo.Gemstones");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "MetalId", "dbo.Metals");
            DropIndex("dbo.Products", new[] { "MetalId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropColumn("dbo.Products", "MetalId");
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.OrderItems", "ProductId");
            CreateIndex("dbo.ProductVariants", "MetalId");
            CreateIndex("dbo.ProductVariants", "ProductId");
            CreateIndex("dbo.OrderItems", "ProductVariantId");
            AddForeignKey("dbo.Products", "GemstoneId", "dbo.Gemstones", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductVariants", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "ProductVariantId", "dbo.ProductVariants", "Id");
            AddForeignKey("dbo.ProductVariants", "MetalId", "dbo.Metals", "Id", cascadeDelete: true);
        }
    }
}
