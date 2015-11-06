using System;
using e10.Shared.Models;
using e10.Shared.Repository;
using e10.Shared.Services;
using e10.Shared.Services.Abstraction;

namespace Warehouse.Data.Services
{
    public class WebMemberService : MemberService, IWebMemberService
    {
        private readonly IInviteRepository _inviteRepository;

        public WebMemberService(IInviteRepository inviteRepository) : base(inviteRepository)
        {
            _inviteRepository = inviteRepository;
        }

        public override InviteCodeViewModel AcceptInvitation(string code)
        {
            return base.AcceptInvitation(code);
        }

        public void Create(SiteUserCreateViewModel siteUserCreateViewModel, int? inviterId)
        {
            throw new NotImplementedException();
        }
    }
}