namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoutercouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Course", "CourseCountOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Course", "CourseCountOrder");
        }
    }
}
