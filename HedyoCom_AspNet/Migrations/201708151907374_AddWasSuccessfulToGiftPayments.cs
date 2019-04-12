namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWasSuccessfulToGiftPayments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiftPayments", "WasSuccessful", c => c.Boolean(nullable: false));
            AddColumn("dbo.GiftPayments", "Error", c => c.String(defaultValue:null));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiftPayments", "Error");
            DropColumn("dbo.GiftPayments", "WasSuccessful");
        }
    }
}
