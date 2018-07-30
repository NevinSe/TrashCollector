namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeDateTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PickUps", "PickUpDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PickUps", "PickUpDate", c => c.DateTime(nullable: false));
        }
    }
}
