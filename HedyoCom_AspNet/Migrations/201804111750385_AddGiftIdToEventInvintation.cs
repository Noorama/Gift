namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGiftIdToEventInvintation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GiftPayments", "Gift_Id", "dbo.Gifts");
            DropIndex("dbo.GiftPayments", new[] { "Gift_Id" });
            RenameColumn(table: "dbo.GiftPayments", name: "Gift_Id", newName: "GiftId");
            AlterColumn("dbo.GiftPayments", "GiftId", c => c.Int(nullable: false));
            CreateIndex("dbo.GiftPayments", "GiftId");
            AddForeignKey("dbo.GiftPayments", "GiftId", "dbo.Gifts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GiftPayments", "GiftId", "dbo.Gifts");
            DropIndex("dbo.GiftPayments", new[] { "GiftId" });
            AlterColumn("dbo.GiftPayments", "GiftId", c => c.Int());
            RenameColumn(table: "dbo.GiftPayments", name: "GiftId", newName: "Gift_Id");
            CreateIndex("dbo.GiftPayments", "Gift_Id");
            AddForeignKey("dbo.GiftPayments", "Gift_Id", "dbo.Gifts", "Id");
        }
    }
}
