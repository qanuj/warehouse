using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using e10.Shared.Repository;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e10.Shared.Data
{
    public abstract class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserLogin, UserRole, IdentityUserClaim>
    {
        protected ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            InviteRepository.Register(modelBuilder);
            FaqRepository.Register(modelBuilder);
            CountryRepository.Register(modelBuilder);
            VisitRepository.Register(modelBuilder);

            //SubscriberRepository.Register(modelBuilder);
            TransactionRepository.Register(modelBuilder);
            SubscriptionRepository.Register(modelBuilder);
        }
    }
}
