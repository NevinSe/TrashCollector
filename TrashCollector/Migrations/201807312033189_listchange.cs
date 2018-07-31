namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listchange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PickUps", "PickUpDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PickUps", "PickUpDate", c => c.DateTime());
        }
    }
}
