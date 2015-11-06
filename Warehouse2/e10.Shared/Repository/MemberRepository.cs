using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using System.Linq;

namespace e10.Shared.Repository
{
    public interface IMemberRepository<T> : IRepository<T> where T: Member
    {
        T ByUserId(string userId);
        T ByEmail(string email);
    }

    public class MemberRepository<T> : EfRepository<T>, IMemberRepository<T> where T : Member
    {
        public MemberRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }

        public T ByUserId(string userId)
        {
            return All.FirstOrDefault(x => x.OwnerId == userId);
        }

        public T ByEmail(string email)
        {
            return All.FirstOrDefault(x => x.Owner.Email == email);
        }
    }
}