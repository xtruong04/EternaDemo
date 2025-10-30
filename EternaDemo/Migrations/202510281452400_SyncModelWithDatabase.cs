namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncModelWithDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Orders", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Orders", new[] { "Address_Id" });
            DropIndex("dbo.OrderItems", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Orders", name: "ShippingAddressId", newName: "AddressId");
            RenameIndex(table: "dbo.Orders", name: "IX_ShippingAddressId", newName: "IX_AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Product_Id", c => c.Int());
            AddColumn("dbo.Orders", "Address_Id", c => c.Int());
            RenameIndex(table: "dbo.Orders", name: "IX_AddressId", newName: "IX_ShippingAddressId");
            RenameColumn(table: "dbo.Orders", name: "AddressId", newName: "ShippingAddressId");
            CreateIndex("dbo.OrderItems", "Product_Id");
            CreateIndex("dbo.Orders", "Address_Id");
            AddForeignKey("dbo.Orders", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products", "Id");
        }
    }
}
