using System.Collections.Generic;
using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface INotificationService
    {
        void Welcome(UserCodeViewModel model);
        void PasswordRecovery(UserCodeViewModel model);
        void Invite(IEnumerable<InviteCodeViewModel> invitees, string by);
        void Feedback(FeedbackCreateViewModel model);
    }
}