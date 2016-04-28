namespace CH.Spartan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_History : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Histories", "DeviceDayKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Histories", "DeviceDayKey", c => c.String(maxLength: 50, storeType: "nvarchar"));
        }
    }
}
