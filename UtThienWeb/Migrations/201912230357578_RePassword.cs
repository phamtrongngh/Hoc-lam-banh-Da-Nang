namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RePassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountRePassword", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "AccountRePassword");
        }
    }
}
