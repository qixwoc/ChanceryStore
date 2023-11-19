namespace ChanceryStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class g : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "OutDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "OutDate");
        }
    }
}
