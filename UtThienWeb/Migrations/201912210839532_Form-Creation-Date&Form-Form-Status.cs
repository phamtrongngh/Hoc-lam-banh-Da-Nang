namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormCreationDateFormFormStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "FormCreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Form", "FormStatus", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "FormStatus");
            DropColumn("dbo.Form", "FormCreationDate");
        }
    }
}
