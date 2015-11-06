using System.Data.Entity;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{

    public interface IFaqRepository : IRepository<Faq>
    {
    }

    public class FaqRepository : EfRepository<Faq>, IFaqRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public FaqRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faq>().HasKey(x => x.Id);
        }
    }
}