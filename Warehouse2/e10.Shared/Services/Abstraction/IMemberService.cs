using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface IMemberService : IService
    {
        InviteCodeViewModel AcceptInvitation(string code);
    }
}