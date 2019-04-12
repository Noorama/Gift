namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeddingParentNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weddings", "MaleParentNames", c => c.String());
            AddColumn("dbo.Weddings", "FemaleParentNames", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Weddings", "FemaleParentNames");
            DropColumn("dbo.Weddings", "MaleParentNames");
        }
    }
}
