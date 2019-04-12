namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GiftImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 12, scale: 2),
                        Image_Id = c.Int(),
                        Wedding_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GiftImages", t => t.Image_Id)
                .ForeignKey("dbo.Weddings", t => t.Wedding_Id)
                .Index(t => t.Image_Id)
                .Index(t => t.Wedding_Id);
            
            CreateTable(
                "dbo.GiftPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayerEmail = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        ReceiptInfo = c.String(),
                        Date = c.DateTime(nullable: false),
                        Gift_Id = c.Int(),
                        PayerUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gifts", t => t.Gift_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PayerUser_Id)
                .Index(t => t.Gift_Id)
                .Index(t => t.PayerUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Weddings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaleName = c.String(),
                        MaleSurname = c.String(),
                        FemaleName = c.String(),
                        FemaleSurname = c.String(),
                        Place = c.String(),
                        Date = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.WeddingInvitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InviteeEmail = c.String(),
                        Payload = c.String(),
                        EverAccepted = c.Boolean(nullable: false),
                        Wedding_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Weddings", t => t.Wedding_Id)
                .Index(t => t.Wedding_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Weddings", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WeddingInvitations", "Wedding_Id", "dbo.Weddings");
            DropForeignKey("dbo.Gifts", "Wedding_Id", "dbo.Weddings");
            DropForeignKey("dbo.GiftPayments", "PayerUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GiftPayments", "Gift_Id", "dbo.Gifts");
            DropForeignKey("dbo.Gifts", "Image_Id", "dbo.GiftImages");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.WeddingInvitations", new[] { "Wedding_Id" });
            DropIndex("dbo.Weddings", new[] { "Owner_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.GiftPayments", new[] { "PayerUser_Id" });
            DropIndex("dbo.GiftPayments", new[] { "Gift_Id" });
            DropIndex("dbo.Gifts", new[] { "Wedding_Id" });
            DropIndex("dbo.Gifts", new[] { "Image_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.WeddingInvitations");
            DropTable("dbo.Weddings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.GiftPayments");
            DropTable("dbo.Gifts");
            DropTable("dbo.GiftImages");
        }
    }
}
