using System.Data.Entity;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{

    public interface IVisitRepository : IRepository<Visit>
    {
    }
    public abstract class VisitRepository : EfRepository<Visit>, IVisitRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        protected VisitRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }

        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visit>().HasKey(x => x.Id);
        }
    }
}