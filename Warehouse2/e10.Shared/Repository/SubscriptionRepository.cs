using System.Data.Entity;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
    public class SubscriptionRepository : EfRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>().HasKey(x => x.Id);
        }
    }

    public interface ISubscriptionRepository : IRepository<Subscription>
    {

    }
}
