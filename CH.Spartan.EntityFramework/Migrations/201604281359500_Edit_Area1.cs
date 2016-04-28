namespace CH.Spartan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Area1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Areas", "Points", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Areas", "Points", c => c.String(maxLength: 500, storeType: "nvarchar"));
        }
    }
}
