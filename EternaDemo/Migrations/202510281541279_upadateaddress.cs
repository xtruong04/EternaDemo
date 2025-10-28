namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadateaddress : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Addresses", "District");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "District", c => c.String(nullable: false));
        }
    }
}
