namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newstuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickUps", "SuspendPickUpStart", c => c.DateTime());
            AddColumn("dbo.PickUps", "SuspendPickUpEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickUps", "SuspendPickUpEnd");
            DropColumn("dbo.PickUps", "SuspendPickUpStart");
        }
    }
}
