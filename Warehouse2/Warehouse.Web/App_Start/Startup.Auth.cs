using e10.Data;
using e10.Shared;
using e10.Shared.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Warehouse.Web
{
    public partial class Startup
    {
        internal static string FacebookNamespace = "conscripthire";
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static OAuthWebConfigProvider OAuth { get; private set; }
        public static string PublicClientId { get; private set; }
        public static OAuthWebConfigProvider SocialProvider { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            OAuth = new OAuthWebConfigProvider();
            PublicClientId = "e10";
            OAuthOptions = SecurityManager.Setup(app, PublicClientId, ApplicationDataContext.Create);
            SocialProvider = SecurityManager.SetupSocial(app, OAuth);
        }
    }
}