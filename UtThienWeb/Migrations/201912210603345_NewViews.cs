namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewViews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "NewsViews", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "NewsViews");
        }
    }
}
