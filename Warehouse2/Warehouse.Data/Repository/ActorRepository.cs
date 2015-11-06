using System.Data.Entity;
using Warehouse.Data.Core;
using e10.Shared.Data.Abstraction;
using e10.Shared.Repository;

namespace Warehouse.Data.Repository
{
    public interface IActorRepository : IMemberRepository<Actor>
    {

    }

    public class ActorRepository : MemberRepository<Actor>, IActorRepository
    {
        public ActorRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {

        }
    }
}