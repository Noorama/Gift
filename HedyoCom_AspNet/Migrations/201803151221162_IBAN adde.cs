namespace HedyoCom_AspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IBANadde : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "IBAN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "IBAN");
        }
    }
}
