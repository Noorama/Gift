namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSocialShareLinkToWeddingInvitation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weddings", "SocialAccessToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Weddings", "SocialAccessToken");
        }
    }
}
