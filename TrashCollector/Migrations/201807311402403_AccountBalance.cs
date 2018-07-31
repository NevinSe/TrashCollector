namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "AccountBalance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "AccountBalance");
        }
    }
}
