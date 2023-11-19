namespace ChanceryStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "LastEntryDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LastEntryDateTime", c => c.DateTime(nullable: false));
        }
    }
}
