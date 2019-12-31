namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Course", "CourseQuantity", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Course", "CourseQuantity");
        }
    }
}
