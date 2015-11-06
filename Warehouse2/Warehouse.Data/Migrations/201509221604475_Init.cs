namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FullName = c.String(),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Invites",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(),
                    Name = c.String(),
                    Email = c.String(),
                    IsCompleted = c.Boolean(nullable: false),
                    Completed = c.DateTime(),
                    InviterId = c.Int(nullable: false),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.InviterId, cascadeDelete: true)
                .Index(t => t.InviterId);

            CreateTable(
                "dbo.Members",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Profile = c.String(),
                    Social_Twitter = c.String(),
                    Social_Facebook = c.String(),
                    Social_Yahoo = c.String(),
                    Social_Google = c.String(),
                    Social_LinkedIn = c.String(),
                    Social_Rss = c.String(),
                    Social_WebSite = c.String(),
                    About = c.String(),
                    OwnerId = c.String(maxLength: 128),
                    Complete = c.Int(nullable: false),
                    FullName = c.String(),
                    Email = c.String(),
                    Mobile = c.String(),
                    AlternateNumber = c.String(),
                    Address = c.String(),
                    PinCode = c.String(),
                    PictureUrl = c.String(),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);

            CreateTable(
                "dbo.Subscriptions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubscriberId = c.Int(nullable: false),
                    Start = c.DateTime(nullable: false),
                    End = c.DateTime(nullable: false),
                    PaymentId = c.Int(),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transactions", t => t.PaymentId)
                .ForeignKey("dbo.Members", t => t.SubscriberId, cascadeDelete: true)
                .Index(t => t.SubscriberId)
                .Index(t => t.PaymentId);

            CreateTable(
                "dbo.Transactions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Reason = c.String(),
                    IsSuccess = c.Boolean(nullable: false),
                    Credit = c.Int(nullable: false),
                    Code = c.String(),
                    Name = c.String(),
                    PaymentCapture = c.String(),
                    UserId = c.String(maxLength: 128),
                    Amount = c.Single(nullable: false),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    Gateway = c.String(),
                    Capture = c.String(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Body = c.String(),
                    TopicId = c.Int(nullable: false),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId);

            CreateTable(
                "dbo.Topics",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Countries",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Code = c.String(),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SiteConfigs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Payment_Key = c.String(),
                    Payment_Salt = c.String(),
                    Payment_MerchantId = c.String(),
                    Payment_Url = c.String(),
                    Credit_Rate = c.Int(nullable: false),
                    Credit_Validity = c.Int(nullable: false),
                    Tax_Name = c.String(),
                    Tax_Rate = c.Double(nullable: false),
                    Notification_Feedback = c.String(),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    Advertisement_Highlight_Rate = c.Int(),
                    Advertisement_Highlight_Validity = c.Int(),
                    Advertisement_Featured_Rate = c.Int(),
                    Advertisement_Featured_Validity = c.Int(),
                    Advertisement_Global_Rate = c.Int(),
                    Advertisement_Global_Validity = c.Int(),
                    Advertisement_Advertise_Rate = c.Int(),
                    Advertisement_Advertise_Validity = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Banners",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ConfigId = c.Int(nullable: false),
                    Title = c.String(),
                    Description = c.String(),
                    Link = c.String(),
                    IsHidden = c.Boolean(nullable: false),
                    Picture = c.String(),
                    Sequence = c.Int(nullable: false),
                    LinkText = c.String(),
                    Rotation1 = c.String(),
                    Rotation2 = c.String(),
                    Scale1 = c.String(),
                    Scale2 = c.String(),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SiteConfigs", t => t.ConfigId, cascadeDelete: true)
                .Index(t => t.ConfigId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Banners", "ConfigId", "dbo.SiteConfigs");
            DropForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Subscriptions", "SubscriberId", "dbo.Members");
            DropForeignKey("dbo.Subscriptions", "PaymentId", "dbo.Transactions");
            DropForeignKey("dbo.Members", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invites", "InviterId", "dbo.Members");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Banners", new[] { "ConfigId" });
            DropIndex("dbo.Posts", new[] { "TopicId" });
            DropIndex("dbo.Transactions", new[] { "UserId" });
            DropIndex("dbo.Subscriptions", new[] { "PaymentId" });
            DropIndex("dbo.Subscriptions", new[] { "SubscriberId" });
            DropIndex("dbo.Members", new[] { "OwnerId" });
            DropIndex("dbo.Invites", new[] { "InviterId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Banners");
            DropTable("dbo.SiteConfigs");
            DropTable("dbo.Countries");
            DropTable("dbo.Topics");
            DropTable("dbo.Posts");
            DropTable("dbo.Transactions");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Members");
            DropTable("dbo.Invites");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
