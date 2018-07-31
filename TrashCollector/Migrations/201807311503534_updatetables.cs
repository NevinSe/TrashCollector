namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "CustomerAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.PickUps", "PickCustomerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickUps", "PickCustomerId");
            DropColumn("dbo.Addresses", "CustomerAddressId");
        }
    }
}
