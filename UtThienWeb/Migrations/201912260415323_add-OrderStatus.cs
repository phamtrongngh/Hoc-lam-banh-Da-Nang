namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOrderStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OrderStatus", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "OrderStatus");
        }
    }
}
