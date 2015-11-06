using e10.Shared.Data.Abstraction;

namespace Warehouse.Data.Core
{
    public class ActorVisit : Visit
    {
        public Actor Actor { get; set; }
        public int ActorId { get; set; }
    }
}