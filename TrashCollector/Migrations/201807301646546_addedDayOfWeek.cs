namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDayOfWeek : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickUps", "DayOfWeek", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickUps", "DayOfWeek");
        }
    }
}
