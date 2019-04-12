namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMaleFemaleNames : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Weddings", "MaleName");
            DropColumn("dbo.Weddings", "MaleSurname");
            DropColumn("dbo.Weddings", "FemaleName");
            DropColumn("dbo.Weddings", "FemaleSurname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Weddings", "FemaleSurname", c => c.String());
            AddColumn("dbo.Weddings", "FemaleName", c => c.String());
            AddColumn("dbo.Weddings", "MaleSurname", c => c.String());
            AddColumn("dbo.Weddings", "MaleName", c => c.String());
        }
    }
}
