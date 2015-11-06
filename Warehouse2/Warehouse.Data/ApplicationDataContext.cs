using System;
using System.Data.Entity;
using Warehouse.Data.Repository;
using e10.Shared.Data;
using e10.Shared.Repository;

namespace Warehouse.Data
{
    public class ApplicationDataContext : ApplicationDbContext
    {
        public ApplicationDataContext() : base("DefaultConnection") { }
        public static ApplicationDataContext Create()
        {
            return new ApplicationDataContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigRepository.Register(modelBuilder);

        }

        public static string Upgrade()
        {
            try
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDataContext, Migrations.Configuration>());
                using (var db = new ApplicationDataContext())
                {
                    db.Database.Initialize(true);
                    return "Completed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
