namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormExperience : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Form", "FormExperience", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Form", "FormExperience");
        }
    }
}
