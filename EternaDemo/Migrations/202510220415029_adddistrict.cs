namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddistrict : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "District", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "District");
        }
    }
}
