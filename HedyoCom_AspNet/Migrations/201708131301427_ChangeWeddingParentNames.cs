namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeWeddingParentNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weddings", "MaleFatherName", c => c.String());
            AddColumn("dbo.Weddings", "MaleMotherName", c => c.String());
            AddColumn("dbo.Weddings", "FemaleFatherName", c => c.String());
            AddColumn("dbo.Weddings", "FemaleMotherName", c => c.String());
            DropColumn("dbo.Weddings", "MaleParentNames");
            DropColumn("dbo.Weddings", "FemaleParentNames");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Weddings", "FemaleParentNames", c => c.String());
            AddColumn("dbo.Weddings", "MaleParentNames", c => c.String());
            DropColumn("dbo.Weddings", "FemaleMotherName");
            DropColumn("dbo.Weddings", "FemaleFatherName");
            DropColumn("dbo.Weddings", "MaleMotherName");
            DropColumn("dbo.Weddings", "MaleFatherName");
        }
    }
}
