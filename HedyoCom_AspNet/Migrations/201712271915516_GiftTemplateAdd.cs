namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiftTemplateAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GiftTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 12, scale: 2),
                        Name = c.String(),
                        GiftImage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GiftImages", t => t.GiftImage_Id)
                .Index(t => t.GiftImage_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GiftTemplates", "GiftImage_Id", "dbo.GiftImages");
            DropIndex("dbo.GiftTemplates", new[] { "GiftImage_Id" });
            DropTable("dbo.GiftTemplates");
        }
    }
}
