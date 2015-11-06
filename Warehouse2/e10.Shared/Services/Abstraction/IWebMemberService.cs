using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface IWebMemberService : IMemberService
    {
        void Create(SiteUserCreateViewModel siteUserCreateViewModel, int? inviterId);
    }
}