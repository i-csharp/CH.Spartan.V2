namespace CH.Spartan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Node : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nodes", "HistoryConnectionStringRead", c => c.String(maxLength: 250, storeType: "nvarchar"));
            AddColumn("dbo.Nodes", "HistoryConnectionStringWrite", c => c.String(maxLength: 250, storeType: "nvarchar"));
            AlterColumn("dbo.Nodes", "HistoryTableName", c => c.String(maxLength: 100, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nodes", "HistoryTableName", c => c.String(maxLength: 250, storeType: "nvarchar"));
            DropColumn("dbo.Nodes", "HistoryConnectionStringWrite");
            DropColumn("dbo.Nodes", "HistoryConnectionStringRead");
        }
    }
}
