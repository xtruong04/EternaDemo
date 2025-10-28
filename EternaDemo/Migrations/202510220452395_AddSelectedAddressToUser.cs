namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSelectedAddressToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SelectedAddressId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "SelectedAddressId");
            AddForeignKey("dbo.AspNetUsers", "SelectedAddressId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "SelectedAddressId", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "SelectedAddressId" });
            DropColumn("dbo.AspNetUsers", "SelectedAddressId");
        }
    }
}
