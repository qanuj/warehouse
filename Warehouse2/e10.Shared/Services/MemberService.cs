using System;
using e10.Shared.Models;
using e10.Shared.Repository;
using e10.Shared.Services.Abstraction;

namespace e10.Shared.Services
{
    public class MemberService : IMemberService
    {
        private readonly IInviteRepository _inviteRepository;

        public MemberService(IInviteRepository inviteRepository)
        {
            _inviteRepository = inviteRepository;
        }

        public virtual InviteCodeViewModel AcceptInvitation(string code)
        {
            var invite = _inviteRepository.ByCode(code);
            if (invite == null || invite.IsCompleted) return null;
            invite.IsCompleted = true;
            invite.Completed = DateTime.UtcNow;
            _inviteRepository.Update(invite);
            _inviteRepository.SaveChanges();

            //var benchInvite = invite as BenchInvite;

            return new InviteCodeViewModel
            {
                Email = invite.Email,
                Name = invite.Name,
                Code = invite.Code,
                InviterId = invite.InviterId
            };
        }
    }
}