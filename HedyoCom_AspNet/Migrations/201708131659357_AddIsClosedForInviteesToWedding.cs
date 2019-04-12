namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsClosedForInviteesToWedding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weddings", "IsClosedForInvitees", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Weddings", "IsClosedForInvitees");
        }
    }
}
