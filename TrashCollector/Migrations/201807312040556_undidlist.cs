namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class undidlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickUps", "PickUpDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickUps", "PickUpDate");
        }
    }
}
