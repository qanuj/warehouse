namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ActorVisit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visits",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IpAddress = c.String(),
                    Country = c.String(),
                    City = c.String(),
                    State = c.String(),
                    Browser = c.String(),
                    Referer = c.String(),
                    OperatingSystem = c.String(),
                    IsMobile = c.Boolean(nullable: false),
                    Visitor = c.String(),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    ActorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.ActorId);

            AddColumn("dbo.Members", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }

        public override void Down()
        {
            DropForeignKey("dbo.Visits", "ActorId", "dbo.Members");
            DropIndex("dbo.Visits", new[] { "ActorId" });
            DropColumn("dbo.Members", "Discriminator");
            DropTable("dbo.Visits");
        }
    }
}
