namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pickupupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickUps", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickUps", "Zipcode");
        }
    }
}
