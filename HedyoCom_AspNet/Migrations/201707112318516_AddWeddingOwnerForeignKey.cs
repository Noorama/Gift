namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeddingOwnerForeignKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Weddings", name: "Owner_Id", newName: "OwnerId");
//            AddColumn("dbo.Weddings", "OwnerId", c=> c.String(maxLength: 128));
//            Sql("UPDATE [Weddings] SET [OwnerId] = [Owner_Id]");
//            CreateIndex("dbo.Weddings", "OwnerId", false, "IX_OwnerId");
//            AddForeignKey("dbo.Weddings", "OwnerId","dbo.AspNetUsers","Id");
            RenameIndex(table: "dbo.Weddings", name: "IX_Owner_Id", newName: "IX_OwnerId");

//            DropIndex("dbo.Weddings", "IX_Owner_Id");
//            DropForeignKey("dbo.Weddings", "FK_dbo.Weddings_dbo.AspNetUsers_Owner_Id");
//            DropColumn("dbo.Weddings","Owner_Id");
        }

        public override void Down()
        {
//            DropForeignKey("dbo.Weddings", "FK_dbo.Weddings_dbo.AspNetUsers_OwnerId");
//            DropIndex("dbo.Weddings", "IX_OwnerId");
            RenameIndex(table: "dbo.Weddings", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.Weddings", name: "OwnerId", newName: "Owner_Id");

//            AddColumn("dbo.Weddings", "Owner_Id", c => c.String(maxLength: 128));
//            Sql("UPDATE [Weddings] SET [Owner_Id] = [OwnerId]");
//            AddForeignKey("dbo.Weddings", "Owner_Id", "dbo.AspNetUsers", "Id");
//            CreateIndex("dbo.Weddings", "Owner_Id", false, "IX_Owner_Id");
//            DropColumn("dbo.Weddings", "OwnerId");
        }
    }
}
