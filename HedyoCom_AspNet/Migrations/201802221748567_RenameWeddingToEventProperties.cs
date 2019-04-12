namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameWeddingToEventProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gifts", "Wedding_Id", "dbo.Weddings");
            DropForeignKey("dbo.WeddingInvitations", "Wedding_Id", "dbo.Weddings");
            DropIndex("dbo.Gifts", new[] { "Wedding_Id" });
            DropIndex("dbo.WeddingInvitations", new[] { "Wedding_Id" });
            RenameColumn(table: "dbo.Gifts", name: "Wedding_Id", newName: "EventId");
            RenameColumn(table: "dbo.WeddingInvitations", name: "Wedding_Id", newName: "EventId");
            RenameColumn(table: "dbo.GiftPayments", name: "PayerUser_Id", newName: "PayerUserId");
            RenameIndex(table: "dbo.GiftPayments", name: "IX_PayerUser_Id", newName: "IX_PayerUserId");
            AlterColumn("dbo.Gifts", "EventId", c => c.Int(nullable: false));
            AlterColumn("dbo.WeddingInvitations", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.Gifts", "EventId");
            CreateIndex("dbo.WeddingInvitations", "EventId");
            AddForeignKey("dbo.Gifts", "EventId", "dbo.Weddings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WeddingInvitations", "EventId", "dbo.Weddings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeddingInvitations", "EventId", "dbo.Weddings");
            DropForeignKey("dbo.Gifts", "EventId", "dbo.Weddings");
            DropIndex("dbo.WeddingInvitations", new[] { "EventId" });
            DropIndex("dbo.Gifts", new[] { "EventId" });
            AlterColumn("dbo.WeddingInvitations", "EventId", c => c.Int());
            AlterColumn("dbo.Gifts", "EventId", c => c.Int());
            RenameIndex(table: "dbo.GiftPayments", name: "IX_PayerUserId", newName: "IX_PayerUser_Id");
            RenameColumn(table: "dbo.GiftPayments", name: "PayerUserId", newName: "PayerUser_Id");
            RenameColumn(table: "dbo.WeddingInvitations", name: "EventId", newName: "Wedding_Id");
            RenameColumn(table: "dbo.Gifts", name: "EventId", newName: "Wedding_Id");
            CreateIndex("dbo.WeddingInvitations", "Wedding_Id");
            CreateIndex("dbo.Gifts", "Wedding_Id");
            AddForeignKey("dbo.WeddingInvitations", "Wedding_Id", "dbo.Weddings", "Id");
            AddForeignKey("dbo.Gifts", "Wedding_Id", "dbo.Weddings", "Id");
        }
    }
}
