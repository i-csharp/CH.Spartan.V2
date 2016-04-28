namespace CH.Spartan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_User1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "Avatar", c => c.String(unicode: false));
            DropColumn("dbo.AbpUsers", "IsInitUserName");
            DropColumn("dbo.AbpUsers", "IsInitPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbpUsers", "IsInitPassword", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbpUsers", "IsInitUserName", c => c.Boolean(nullable: false));
            DropColumn("dbo.AbpUsers", "Avatar");
        }
    }
}
