namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Weddings", "EventTypeId", c => c.Int());
            CreateIndex("dbo.Weddings", "EventTypeId");
            AddForeignKey("dbo.Weddings", "EventTypeId", "dbo.EventTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Weddings", "EventTypeId", "dbo.EventTypes");
            DropIndex("dbo.Weddings", new[] { "EventTypeId" });
            DropColumn("dbo.Weddings", "EventTypeId");
            DropTable("dbo.EventTypes");
        }
    }
}
