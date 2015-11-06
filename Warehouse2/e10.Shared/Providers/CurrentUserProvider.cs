using System.Collections.Generic;
using System.Web;
using e10.Shared.Data.Abstraction;
using e10.Shared.Extensions;
using e10.Shared.Security;
using Microsoft.AspNet.Identity;

namespace e10.Shared.Providers
{
    public class CurrentUserProvider : IUserProvider
    {
        private readonly ApplicationUserManager _userManager;

        public CurrentUserProvider(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public string UserId
        {
            get
            {
                var identity = HttpContext.Current.User.Identity;
                if (identity != null) return identity.GetUserId();
                return string.Empty;
            }
        }

        static readonly Dictionary<string,int> userMapping=new Dictionary<string, int>();

        public int? ActorId
        {
            get
            {
                var identity = HttpContext.Current.User.Identity;
                if (identity != null)
                {
                    if (userMapping.ContainsKey(UserId)) return userMapping[UserId];

                    var user=_userManager.FindById(UserId);
                    if (user == null) return null;
                    userMapping.Add(UserId, user.ActorId);

                    return userMapping[UserId];
                }
                return null;
            }
        }

        public string UserIdByEmail(string email)
        {
             var user=_userManager.FindByEmail(email);
            if (user != null) return user.Id;
            return string.Empty;
        }

        public string UserEmailById(string id)
        {
            var user = _userManager.FindById(id);
            if (user != null) return user.Email;
            return string.Empty;
        }
    }
}