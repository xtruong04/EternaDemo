namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateaddress : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "ShippingAddressId", newName: "AddressId");
            RenameIndex(table: "dbo.Orders", name: "IX_ShippingAddressId", newName: "IX_AddressId");
            AddColumn("dbo.Orders", "Address_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Address_Id");
            AddForeignKey("dbo.Orders", "Address_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Orders", new[] { "Address_Id" });
            DropColumn("dbo.Orders", "Address_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_AddressId", newName: "IX_ShippingAddressId");
            RenameColumn(table: "dbo.Orders", name: "AddressId", newName: "ShippingAddressId");
        }
    }
}
