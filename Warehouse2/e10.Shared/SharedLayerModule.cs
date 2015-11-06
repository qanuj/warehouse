using Autofac;
using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using e10.Shared.Providers;
using e10.Shared.Repository;
using e10.Shared.Security;
using e10.Shared.Services;
using e10.Shared.Services.Abstraction;
using Microsoft.Owin.Logging;

namespace e10.Shared
{
    public class SharedLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserStore>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleStore>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().As<IUserService>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<SendGridEmailService>().As<IIdentityEmailMessageService>().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentitySmsMessageService>().InstancePerRequest();
            builder.RegisterType<DoNotReplyAte10EmailConfigProvider>().As<IEmailConfigProvider>().InstancePerRequest();
            builder.RegisterType<ElmahLogger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<CurrentUserProvider>().As<IUserProvider>().InstancePerRequest();

            builder.RegisterType<InviteRepository>().As<IInviteRepository>().As<IRepository<Invite>>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>().As<IRepository<Transaction>>();
            builder.RegisterType<FaqRepository>().As<IFaqRepository>().As<IRepository<Faq>>();
            builder.RegisterType<FeedbackRepository>().As<IFeedbackRepository>().As<IRepository<Feedback>>();
            builder.RegisterType<BannerRepository>().As<IBannerRepository>().As<IRepository<Banner>>();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>().As<IRepository<Country>>();
            builder.RegisterType<SubscriptionRepository>().As<ISubscriptionRepository>().As<IRepository<Subscription>>();
            builder.RegisterType<EmailySubscriptionProvider>().As<ISubscriptionProvider>();
            builder.RegisterType<XeroInvoiceProvider>().As<IInvoiceProvider>();
            builder.RegisterType<PayPalService>().As<IPayPalService>();
            builder.RegisterType<OAuthWebConfigProvider>().AsSelf();

            builder.RegisterType<SiteService>().As<ISiteService>();
        }
    }
}
