using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
    public interface IInviteRepository : IRepository<Invite>
    {
        Task<Invite> ByCodeAsync(string code);
        Invite ByCode(string code);
    }

    public class InviteRepository : EfRepository<Invite>, IInviteRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public InviteRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }

        public Task<Invite> ByCodeAsync(string code)
        {
            return All.FirstOrDefaultAsync(x => x.Code == code);
        }

        public Invite ByCode(string code)
        {
            return All.FirstOrDefault(x => x.Code == code);
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invite>().HasKey(x => x.Id);
        }
    }
}