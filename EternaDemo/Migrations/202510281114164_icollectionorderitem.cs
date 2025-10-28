namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class icollectionorderitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Product_Id", c => c.Int());
            CreateIndex("dbo.OrderItems", "Product_Id");
            AddForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products");
            DropIndex("dbo.OrderItems", new[] { "Product_Id" });
            DropColumn("dbo.OrderItems", "Product_Id");
        }
    }
}
