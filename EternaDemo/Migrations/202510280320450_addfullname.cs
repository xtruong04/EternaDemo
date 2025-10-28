namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfullname : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "SelectedAddressId", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "SelectedAddressId" });
            AddColumn("dbo.Addresses", "Phone", c => c.String());
            DropColumn("dbo.Addresses", "District");
            DropColumn("dbo.AspNetUsers", "SelectedAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "SelectedAddressId", c => c.Int());
            AddColumn("dbo.Addresses", "District", c => c.String(nullable: false));
            DropColumn("dbo.Addresses", "Phone");
            CreateIndex("dbo.AspNetUsers", "SelectedAddressId");
            AddForeignKey("dbo.AspNetUsers", "SelectedAddressId", "dbo.Addresses", "Id");
        }
    }
}
