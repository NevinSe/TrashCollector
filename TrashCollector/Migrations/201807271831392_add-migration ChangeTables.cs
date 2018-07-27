namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationChangeTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "UserName", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "UserName" });
            AlterColumn("dbo.Customers", "UserName", c => c.String());
            DropColumn("dbo.AspNetRoles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Customers", "UserName", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "UserName");
            AddForeignKey("dbo.Customers", "UserName", "dbo.AspNetUsers", "Id");
        }
    }
}
