namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PickUps", "CustomerId", "dbo.Customers");
            DropIndex("dbo.PickUps", new[] { "CustomerId" });
            AddColumn("dbo.Customers", "PickId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "PickId");
            AddForeignKey("dbo.Customers", "PickId", "dbo.PickUps", "PickUpId", cascadeDelete: true);
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.PickUps", "CustomerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Customers", "PickId", "dbo.PickUps");
            DropIndex("dbo.Customers", new[] { "PickId" });
            
            CreateIndex("dbo.PickUps", "CustomerId");
            AddForeignKey("dbo.PickUps", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
