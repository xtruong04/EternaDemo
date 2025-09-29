namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gemstones", "Id", "dbo.Products");
            DropForeignKey("dbo.Certificates", "GemstoneId", "dbo.Gemstones");
            DropIndex("dbo.Gemstones", new[] { "Id" });
            DropPrimaryKey("dbo.Gemstones");
            AlterColumn("dbo.Gemstones", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Gemstones", "Id");
            CreateIndex("dbo.Products", "GemstoneId");
            AddForeignKey("dbo.Products", "GemstoneId", "dbo.Gemstones", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Certificates", "GemstoneId", "dbo.Gemstones", "Id");
            DropColumn("dbo.Gemstones", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gemstones", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Certificates", "GemstoneId", "dbo.Gemstones");
            DropForeignKey("dbo.Products", "GemstoneId", "dbo.Gemstones");
            DropIndex("dbo.Products", new[] { "GemstoneId" });
            DropPrimaryKey("dbo.Gemstones");
            AlterColumn("dbo.Gemstones", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Gemstones", "Id");
            CreateIndex("dbo.Gemstones", "Id");
            AddForeignKey("dbo.Certificates", "GemstoneId", "dbo.Gemstones", "Id");
            AddForeignKey("dbo.Gemstones", "Id", "dbo.Products", "Id");
        }
    }
}
