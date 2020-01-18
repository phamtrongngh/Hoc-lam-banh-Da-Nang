namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codeAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountCurrentCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "AccountCurrentCode");
        }
    }
}
