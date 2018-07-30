namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDatabasePickups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PickUps",
                c => new
                    {
                        PickUpId = c.Int(nullable: false, identity: true),
                        PickUpDate = c.DateTime(nullable: false),
                        Cost = c.Double(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PickUpId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PickUps", "CustomerId", "dbo.Customers");
            DropIndex("dbo.PickUps", new[] { "CustomerId" });
            DropTable("dbo.PickUps");
        }
    }
}
