using System.Linq;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Providers
{
    public interface IUserProvider
    {
        string UserId { get; }
        int? ActorId { get; }
        string UserIdByEmail(string email);
        string UserEmailById(string id);
    }
}