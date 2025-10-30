namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicationUsersDbSet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "District", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Phone", c => c.String());
            DropColumn("dbo.Addresses", "District");
        }
    }
}
