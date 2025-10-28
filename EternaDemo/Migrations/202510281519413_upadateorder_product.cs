namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadateorder_product : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "ShippingAddressId" });
            RenameColumn(table: "dbo.Orders", name: "ShippingAddressId", newName: "AddressId");
            AddColumn("dbo.OrderItems", "Product_Id", c => c.Int());
            AlterColumn("dbo.Orders", "AddressId", c => c.Int());
            CreateIndex("dbo.OrderItems", "Product_Id");
            CreateIndex("dbo.Orders", "AddressId");
            AddForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropIndex("dbo.OrderItems", new[] { "Product_Id" });
            AlterColumn("dbo.Orders", "AddressId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "Product_Id");
            RenameColumn(table: "dbo.Orders", name: "AddressId", newName: "ShippingAddressId");
            CreateIndex("dbo.Orders", "ShippingAddressId");
        }
    }
}
