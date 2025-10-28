namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadateaddress2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "District", c => c.String(nullable: false));
            DropColumn("dbo.Addresses", "Districts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Districts", c => c.String(nullable: false));
            DropColumn("dbo.Addresses", "District");
        }
    }
}
