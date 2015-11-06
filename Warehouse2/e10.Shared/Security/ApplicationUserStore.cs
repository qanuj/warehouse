using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace e10.Shared.Security
{
    public class ApplicationUserStore : UserStore<User, Role, string, IdentityUserLogin, UserRole, IdentityUserClaim>, IUserStore<User>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}