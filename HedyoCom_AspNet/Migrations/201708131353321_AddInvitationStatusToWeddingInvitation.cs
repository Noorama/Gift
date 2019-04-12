namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvitationStatusToWeddingInvitation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WeddingInvitations", "EverDisplayed", c => c.Boolean(nullable: false));
            AddColumn("dbo.WeddingInvitations", "InvitationStatus", c => c.Int(nullable: false, defaultValue:0));
            DropColumn("dbo.WeddingInvitations", "EverAccepted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WeddingInvitations", "EverAccepted", c => c.Boolean(nullable: false));
            DropColumn("dbo.WeddingInvitations", "InvitationStatus");
            DropColumn("dbo.WeddingInvitations", "EverDisplayed");
        }
    }
}
