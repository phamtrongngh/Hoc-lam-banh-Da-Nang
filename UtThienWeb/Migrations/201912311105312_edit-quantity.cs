namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editquantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "OrderQuantity", c => c.Int());
            DropColumn("dbo.Course", "CourseQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Course", "CourseQuantity", c => c.Int());
            DropColumn("dbo.OrderDetails", "OrderQuantity");
        }
    }
}
