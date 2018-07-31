namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeeupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Zipcode");
        }
    }
}
