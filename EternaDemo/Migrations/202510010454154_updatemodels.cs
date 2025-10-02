namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Metals", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Metals", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
