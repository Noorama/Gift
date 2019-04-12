namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGiftImageFilename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiftImages", "Filename", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiftImages", "Filename");
        }
    }
}
