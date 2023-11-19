namespace ChanceryStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "LastEntryDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LastEntryDateTime", c => c.DateTime(nullable: false));
        }
    }
}
