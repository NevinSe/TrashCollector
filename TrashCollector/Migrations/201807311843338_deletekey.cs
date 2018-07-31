namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletekey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "UserName", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "UserName" });
            AlterColumn("dbo.Employees", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "UserName", c => c.String(maxLength: 128));
            CreateIndex("dbo.Employees", "UserName");
            AddForeignKey("dbo.Employees", "UserName", "dbo.AspNetUsers", "Id");
        }
    }
}
