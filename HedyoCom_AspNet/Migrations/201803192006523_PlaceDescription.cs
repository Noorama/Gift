namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaceDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "PlaceDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "PlaceDescription");
        }
    }
}
