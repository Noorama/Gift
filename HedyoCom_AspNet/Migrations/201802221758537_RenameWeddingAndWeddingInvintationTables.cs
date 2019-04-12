namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameWeddingAndWeddingInvintationTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Weddings", newName: "Events");
            RenameTable(name: "dbo.WeddingInvitations", newName: "EventInvitations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.EventInvitations", newName: "WeddingInvitations");
            RenameTable(name: "dbo.Events", newName: "Weddings");
        }
    }
}
