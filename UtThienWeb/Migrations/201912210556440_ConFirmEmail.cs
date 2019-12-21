namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConFirmEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountConfirmEmail", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "AccountConfirmEmail");
        }
    }
}
