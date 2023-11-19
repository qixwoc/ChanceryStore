namespace ChanceryStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Login");
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Type");
            DropColumn("dbo.Users", "LastEntryDateTime");
            DropColumn("dbo.Users", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Image", c => c.Binary());
            AddColumn("dbo.Users", "LastEntryDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Password", c => c.String());
            AddColumn("dbo.Users", "Login", c => c.String());
        }
    }
}
