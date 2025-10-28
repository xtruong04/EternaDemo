namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteshippingaddress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "ShippingAddressId" });
            RenameColumn(table: "dbo.Orders", name: "ShippingAddressId", newName: "ShippingAddress_Id");
            AlterColumn("dbo.Orders", "ShippingAddress_Id", c => c.Int());
            CreateIndex("dbo.Orders", "ShippingAddress_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "ShippingAddress_Id" });
            AlterColumn("dbo.Orders", "ShippingAddress_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "ShippingAddress_Id", newName: "ShippingAddressId");
            CreateIndex("dbo.Orders", "ShippingAddressId");
        }
    }
}
