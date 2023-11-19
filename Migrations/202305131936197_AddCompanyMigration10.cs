namespace ChanceryStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Responsibilities = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "TypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "LastEntryDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Users", "TypeId");
            AddForeignKey("dbo.Users", "TypeId", "dbo.Types", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "TypeId", "dbo.Types");
            DropIndex("dbo.Users", new[] { "TypeId" });
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "LastEntryDateTime");
            DropColumn("dbo.Users", "TypeId");
            DropTable("dbo.Types");
        }
    }
}
