namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class BannerSliderDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "Orientation", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Banners", "Orientation");
        }
    }
}
