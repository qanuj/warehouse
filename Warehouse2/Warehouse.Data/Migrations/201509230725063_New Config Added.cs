namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class NewConfigAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SiteConfigs", "Advertisement_Highlight_Rate", c => c.Int(nullable: false));
            AlterColumn("dbo.SiteConfigs", "Advertisement_Highlight_Validity", c => c.Int(nullable: false));
            AlterColumn("dbo.SiteConfigs", "Advertisement_Featured_Rate", c => c.Int(nullable: false));
            AlterColumn("dbo.SiteConfigs", "Advertisement_Featured_Validity", c => c.Int(nullable: false));
            AlterColumn("dbo.SiteConfigs", "Advertisement_Global_Rate", c => c.Int(nullable: false));
            AlterColumn("dbo.SiteConfigs", "Advertisement_Global_Validity", c => c.Int(nullable: false));
            AlterColumn("dbo.SiteConfigs", "Advertisement_Advertise_Rate", c => c.Int(nullable: false));
            AlterColumn("dbo.SiteConfigs", "Advertisement_Advertise_Validity", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.SiteConfigs", "Advertisement_Advertise_Validity", c => c.Int());
            AlterColumn("dbo.SiteConfigs", "Advertisement_Advertise_Rate", c => c.Int());
            AlterColumn("dbo.SiteConfigs", "Advertisement_Global_Validity", c => c.Int());
            AlterColumn("dbo.SiteConfigs", "Advertisement_Global_Rate", c => c.Int());
            AlterColumn("dbo.SiteConfigs", "Advertisement_Featured_Validity", c => c.Int());
            AlterColumn("dbo.SiteConfigs", "Advertisement_Featured_Rate", c => c.Int());
            AlterColumn("dbo.SiteConfigs", "Advertisement_Highlight_Validity", c => c.Int());
            AlterColumn("dbo.SiteConfigs", "Advertisement_Highlight_Rate", c => c.Int());
        }
    }
}
