namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadateorder_product : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "ShippingAddressId" });
            AddColumn("dbo.OrderItems", "Product_Id", c => c.Int());
            AlterColumn("dbo.Orders", "AddressId", c => c.Int());
            CreateIndex("dbo.OrderItems", "Product_Id");
            AddForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products");
            DropIndex("dbo.OrderItems", new[] { "Product_Id" });
            AlterColumn("dbo.Orders", "AddressId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "Product_Id");
            CreateIndex("dbo.Orders", "ShippingAddressId");
        }
    }
}
