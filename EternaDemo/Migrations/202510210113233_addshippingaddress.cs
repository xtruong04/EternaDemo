namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addshippingaddress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "ShippingAddress_Id" });
            RenameColumn(table: "dbo.Orders", name: "ShippingAddress_Id", newName: "ShippingAddressId");
            AlterColumn("dbo.Orders", "ShippingAddressId", c => c.Int(nullable: true));
            CreateIndex("dbo.Orders", "ShippingAddressId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "ShippingAddressId" });
            AlterColumn("dbo.Orders", "ShippingAddressId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "ShippingAddressId", newName: "ShippingAddress_Id");
            CreateIndex("dbo.Orders", "ShippingAddress_Id");
        }
    }
}
