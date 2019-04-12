namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveParents : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Weddings", "MaleFatherName");
            DropColumn("dbo.Weddings", "MaleMotherName");
            DropColumn("dbo.Weddings", "FemaleFatherName");
            DropColumn("dbo.Weddings", "FemaleMotherName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Weddings", "FemaleMotherName", c => c.String());
            AddColumn("dbo.Weddings", "FemaleFatherName", c => c.String());
            AddColumn("dbo.Weddings", "MaleMotherName", c => c.String());
            AddColumn("dbo.Weddings", "MaleFatherName", c => c.String());
        }
    }
}
