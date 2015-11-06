using System;
using System.Linq;
using System.Net.Mime;
using System.Security;
using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System.Threading.Tasks;
using e10.Shared.Providers;
using Microsoft.Owin.Logging;

namespace e10.Shared.Security
{
    public class ApplicationUserManager : UserManager<User>, IUserService
    {
        public ApplicationUserManager(ApplicationUserStore store, IIdentitySmsMessageService smsService, IIdentityEmailMessageService emailService, IDataProtectionProvider dataProtectionProvider)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<User>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            this.EmailService = emailService;
            this.SmsService = smsService;
            this.UserTokenProvider = DefaultTokenProvider(dataProtectionProvider);
        }

        public void Setup()
        {
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User>
            {
                MessageFormat = "Your security code is {0}"
            });
            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {

            var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<ApplicationDbContext>()), new SmsService(), 
                context.Get<IIdentityEmailMessageService>(), options.DataProtectionProvider);
            manager.Setup();

            return manager;
        }
        public static IUserTokenProvider<User, string> DefaultTokenProvider(IDataProtectionProvider dataProtector)
        {
            return new DataProtectorTokenProvider<User>(dataProtector.Create("ASP.NET Identity"))
            {
                TokenLifespan = TimeSpan.FromHours(3)
            };
        }

        public async Task<User> CreateGodAsync(string email, string password)
        {
            var user = await FindByEmailAsync(email);
            if (user == null)
            {
                user = new User { UserName = email, Email = email };
                var result = await CreateAsync(user, password);
                if (result.Succeeded)
                {
                    this.AddToRole(user.Id, SecurityManager.God);
                }
            }
            return user;
        }

        public async Task<string> CreateAsync(string email, string password, string role)
        {
            var user = await FindByEmailAsync(email);
            if (user == null)
            {
                user = new User { UserName = email, Email = email };
                await CreateAsync(user, password);
            }
            await AddToRolesAsync(user.Id, new[] { role });
            return user.Id;
        }

        public IQueryable<User> FindByRole(string role)
        {
            return this.Users.Where(x => x.Roles.Any(y => y.RoleId == role));
        }
    }
}