namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCharacter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        Role = c.String(maxLength: 256),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Weddings", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "EventId", "dbo.Weddings");
            DropIndex("dbo.Characters", new[] { "EventId" });
            DropTable("dbo.Characters");
        }
    }
}
