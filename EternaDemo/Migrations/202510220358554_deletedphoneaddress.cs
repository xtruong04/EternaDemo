namespace EternaDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedphoneaddress : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Addresses", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Phone", c => c.String());
        }
    }
}
