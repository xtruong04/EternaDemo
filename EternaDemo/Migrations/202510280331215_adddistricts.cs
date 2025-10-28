namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddistricts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Districts", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "Districts");
        }
    }
}
