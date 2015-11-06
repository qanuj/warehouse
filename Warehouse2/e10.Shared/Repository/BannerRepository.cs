using System.Data.Entity;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
    public interface IBannerRepository : IRepository<Banner>
    {
    }
    public class BannerRepository : EfRepository<Banner>, IBannerRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public BannerRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>().HasKey(x => x.Id);
        }
    }
}