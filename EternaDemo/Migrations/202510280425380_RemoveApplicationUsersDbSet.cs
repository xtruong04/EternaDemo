namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicationUsersDbSet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "District", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "SelectedAddressId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "SelectedAddressId");
            AddForeignKey("dbo.AspNetUsers", "SelectedAddressId", "dbo.Addresses", "Id");
            DropColumn("dbo.Addresses", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Phone", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "SelectedAddressId", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "SelectedAddressId" });
            DropColumn("dbo.AspNetUsers", "SelectedAddressId");
            DropColumn("dbo.Addresses", "District");
        }
    }
}
