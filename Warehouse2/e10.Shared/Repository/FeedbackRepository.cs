using System.Data.Entity;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        void Read(int id, bool what);
    }

    public class FeedbackRepository : EfRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {

        }

        public void Read(int id, bool what)
        {
            var entity = ById(id);
            entity.IsRead = what;
            Update(entity);
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>().HasKey(x => x.Id);
        }
    }
}