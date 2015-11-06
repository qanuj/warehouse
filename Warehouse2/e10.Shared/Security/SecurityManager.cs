using System;
using System.Net.Http;
using System.Threading.Tasks;
using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Owin.Security.Providers.GitHub;
using Owin.Security.Providers.GooglePlus;
using Owin.Security.Providers.LinkedIn;
using System.Security.Claims;
using System.Threading;
using e10.Shared.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.Twitter;
using Newtonsoft.Json;
using Owin.Security.Providers.Dropbox;
using Owin.Security.Providers.GooglePlus.Provider;
using Owin.Security.Providers.Instagram;
using Owin.Security.Providers.Instagram.Provider;
using Owin.Security.Providers.SoundCloud;
using Owin.Security.Providers.SoundCloud.Provider;
using Owin.Security.Providers.Twitch;
using Owin.Security.Providers.Yahoo;

namespace e10.Shared.Security
{

    public class SecurityManager
    {
        public static string God = "Admin";

        public static OAuthAuthorizationServerOptions Setup(IAppBuilder app, string publicClientId, Func<ApplicationDbContext> create)
        {

            app.CreatePerOwinContext<ApplicationDbContext>(create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(publicClientId),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            return OAuthOptions;
        }

        public static OAuthWebConfigProvider SetupSocial(IAppBuilder app, OAuthWebConfigProvider SocialProvider)
        {
            if (!string.IsNullOrWhiteSpace(SocialProvider.GooglePlus.Key))
                app.UseGoogleAuthentication(GetGoogle(SocialProvider.GooglePlus.Key, SocialProvider.GooglePlus.Secret));

            if (!string.IsNullOrWhiteSpace(SocialProvider.LinkedIn.Key))
                app.UseLinkedInAuthentication(GetLinkedIn(SocialProvider.LinkedIn.Key, SocialProvider.LinkedIn.Secret));

            if (!string.IsNullOrWhiteSpace(SocialProvider.GitHub.Key))
                app.UseGitHubAuthentication(GetGithub(SocialProvider.GitHub.Key, SocialProvider.GitHub.Secret));

            if (!string.IsNullOrWhiteSpace(SocialProvider.Facebook.Key))
                app.UseFacebookAuthentication(GetFacebook(SocialProvider.Facebook.Key, SocialProvider.Facebook.Secret,SocialProvider.Facebook.Scope));

            if (!string.IsNullOrWhiteSpace(SocialProvider.Microsoft.Key))
                app.UseMicrosoftAccountAuthentication(GetMicrosoft(SocialProvider.Microsoft.Key, SocialProvider.Microsoft.Secret));

            if (!string.IsNullOrWhiteSpace(SocialProvider.Twitter.Key))
                app.UseTwitterAuthentication(GetTwitter(SocialProvider.Twitter.Key, SocialProvider.Twitter.Secret));

            return SocialProvider;
        }


        private const string GoogleScopes = "openid,profile,email,https://www.googleapis.com/auth/plus.login,https://www.googleapis.com/auth/plus.me,https://www.googleapis.com/auth/userinfo.email,https://www.googleapis.com/auth/userinfo.profile";

        public static GoogleOAuth2AuthenticationOptions GetGoogle(string key, string secret)
        {
            var tmp = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = key,
                ClientSecret = secret,
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = OnAuthenticatedGoogle
                }
            };
            tmp.Scope.AddRange(GoogleScopes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            return tmp;
        }

        private static Task OnAuthenticatedGoogle(GoogleOAuth2AuthenticatedContext ctx)
        {
            TimeSpan expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var jsonText = ctx.User.ToString();
            dynamic d = JsonConvert.DeserializeObject(jsonText);
            var data = new
            {
                provider = "google",
                id = ctx.Id,
                email = ctx.Email,
                username = ctx.Email,
                name = ctx.Name,
                url = ctx.Profile,
                picture = d.image.url,
                d.verified,
                accesstoken = ctx.AccessToken,
                refreshtoken = ctx.RefreshToken,
                expiry = DateTime.Now.Add(expiryDuration),
                extra = d
            };
            ctx.Identity.AddClaim(new Claim("google:about:" + ctx.Id, data.ToJson()));
            return Task.FromResult(0);
        }

        public static LinkedInAuthenticationOptions GetLinkedIn(string key, string secret)
        {
            var tmp = new LinkedInAuthenticationOptions()
            {
                ClientSecret = secret,
                ClientId = key,
                Provider = new LinkedInAuthenticationProvider()
                {
                    OnAuthenticated = OnAuthenticatedLinkedIn
                }
            };
            const string scopes = "r_basicprofile,r_emailaddress,rw_company_admin,w_share";
            tmp.Scope.AddRange(scopes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            return tmp;
        }
        private static Task OnAuthenticatedLinkedIn(LinkedInAuthenticatedContext ctx)
        {
            var expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var data = new
            {
                provider = "linkedin",
                id = ctx.Id,
                username = ctx.UserName,
                email = ctx.Email,
                name = ctx.Name,
                url = ctx.Link,
                picture = string.Format("http://avatars.io/linkedin/{0}", ctx.UserName),
                accesstoken = ctx.AccessToken,
                expiry = DateTime.Now.Add(expiryDuration),
                extra = ctx.User
            };
            ctx.Identity.AddClaim(new Claim("linkedin:about:" + ctx.Id, data.ToJson()));
            return Task.FromResult(0);
        }

        public static MicrosoftAccountAuthenticationOptions GetMicrosoft(string key, string secret)
        {
            return new MicrosoftAccountAuthenticationOptions()
            {
                ClientId = key,
                ClientSecret = secret,
                Provider = new MicrosoftAccountAuthenticationProvider()
                {
                    OnAuthenticated = OnAuthenticatedMicrosoft
                }
            };
        }

        private static Task OnAuthenticatedMicrosoft(MicrosoftAccountAuthenticatedContext ctx)
        {
            var expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var data = new
            {
                provider = "microsoft",
                id = ctx.Id,
                username = ctx.Email,
                email = ctx.Email,
                name = ctx.Name,
                url = "",
                accesstoken = ctx.AccessToken,
                picture = string.Format("http://graph.facebook.com/{0}/picture", ctx.Id),
                verified = (bool?)ctx.User["verified"] ?? false,
                expiry = DateTime.Now.Add(expiryDuration),
                extra = ctx.User
            };
            ctx.Identity.AddClaim(new Claim("microsoft:about:" + ctx.Id, data.ToJson()));
            return Task.FromResult(0);
        }

        public static TwitterAuthenticationOptions GetTwitter(string key, string secret)
        {
            return new TwitterAuthenticationOptions()
            {
                ConsumerKey = key,
                ConsumerSecret = secret,
                Provider = new TwitterAuthenticationProvider{
                    OnAuthenticated = OnAuthenticateTwitter
                },
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(
                new[]{
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                    "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                    "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                    "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                    "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
                    "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
                    "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA
                })
            };
        }

        private static Task OnAuthenticateTwitter(TwitterAuthenticatedContext ctx)
        {
            var data = new
            {
                provider = "twitter",
                id = ctx.UserId,
                username = ctx.ScreenName,
                email = ctx.ScreenName + "@twitter.com",
                name = ctx.ScreenName,
                url = "https://twitter.com/" + ctx.ScreenName,
                picture = string.Format("http://avatars.io/twitter/{0}", ctx.ScreenName),
                verified = true,
                accesstoken = ctx.AccessToken,
                secret = ctx.AccessTokenSecret,
            };
            ctx.Identity.AddClaim(new Claim("twitter:about:" + ctx.UserId, data.ToJson()));
            return Task.FromResult(0);
        }


        public static FacebookAuthenticationOptions GetFacebook(string key, string secret,string scopes)
        {
            var tmp = new FacebookAuthenticationOptions {
                AppSecret = secret,
                AppId = key,
                BackchannelHttpHandler = new FacebookBackChannelHandler(),
                Provider = new FacebookAuthenticationProvider { OnAuthenticated = OnFacebookAuthenticated },
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=verified,link,id,name,email,first_name,last_name,location"
            };
            if (string.IsNullOrWhiteSpace(scopes)){
                scopes = "publish_actions,publish_pages,manage_pages,email";
            }
            tmp.Scope.AddRange(scopes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            return tmp;
        }
        public class FacebookBackChannelHandler : HttpClientHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
            {
                // Replace the RequestUri so it's not malformed
                if (!request.RequestUri.AbsolutePath.Contains("/oauth")){
                    request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }

        private static Task OnFacebookAuthenticated(FacebookAuthenticatedContext ctx)
        {
            var expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var data = new {
                provider = "facebook",
                id = ctx.Id,
                username = ctx.Id,
                email = ctx.Email,
                name = ctx.Name,
                url = ctx.Link,
                picture = string.Format("http://graph.facebook.com/{0}/picture", ctx.Id),
                verified = (bool)ctx.User["verified"],
                accesstoken = ctx.AccessToken,
                expiry = DateTime.Now.Add(expiryDuration),
                extra = ctx.User
            };
            ctx.Identity.AddClaim(new Claim("facebook:about:" + ctx.Id, JsonConvert.SerializeObject(data), "http://www.w3.org/2001/XMLSchema#string"));
            ctx.Identity.AddClaim(new Claim(ClaimTypes.Email, ctx.Email));
            ctx.Identity.AddClaim(new Claim(ClaimTypes.Name, ctx.Name));
            return Task.FromResult(0);
        }

        public static GooglePlusAuthenticationOptions GetGooglePlus(string key, string secret)
        {
            var tmp = new GooglePlusAuthenticationOptions()
            {
                ClientId = key,
                ClientSecret = secret,
                Provider = new GooglePlusAuthenticationProvider
                {
                    OnAuthenticated = OnAuthenticatedGooglePlus
                }
            };
            tmp.Scope.AddRange("".Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            return tmp;
        }

        private static Task OnAuthenticatedGooglePlus(GooglePlusAuthenticatedContext ctx)
        {
            TimeSpan expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var jsonText = ctx.User.ToString();
            dynamic d = JsonConvert.DeserializeObject(jsonText);
            var data = new
            {
                provider = "google",
                id = ctx.Id,
                email = ctx.Email,
                username = ctx.UserName,
                name = ctx.Name,
                url = ctx.Link,
                picture = d.image.url,
                d.verified,
                accesstoken = ctx.AccessToken,
                refreshtoken = ctx.RefreshToken,
                expiry = DateTime.Now.Add(expiryDuration),
                extra = d
            };
            ctx.Identity.AddClaim(new Claim("google:about:" + ctx.Id, data.ToJson()));
            return Task.FromResult(0);
        }


        public static InstagramAuthenticationOptions GetInstagram(string key, string secret)
        {
            var tmp = new InstagramAuthenticationOptions()
            {
                ClientSecret = secret,
                ClientId = key,
                Provider = new InstagramAuthenticationProvider()
                {
                    OnAuthenticated = OnAuthenticatedInstagram
                }
            };
            const string scopes = "basic,comments,relationships,likes";
            tmp.Scope.AddRange(scopes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            return tmp;
        }

        private static Task OnAuthenticatedInstagram(InstagramAuthenticatedContext ctx)
        {
            var data = new
            {
                provider = "instagram",
                id = ctx.Id,
                username = ctx.UserName,
                email = string.Format("{0}@instagram.com", ctx.UserName),
                name = ctx.Name,
                url = string.Format("https://instagram.com/{0}", ctx.UserName),
                picture = ctx.ProfilePicture,
                verified = (bool)ctx.User["verified"],
                accesstoken = ctx.AccessToken,
                expiry = DateTime.UtcNow.AddYears(1),
                extra = ctx.User
            };
            ctx.Identity.AddClaim(new Claim("instagram:about:" + ctx.Id, data.ToJson()));
            return Task.FromResult(0);
        }
        
        public static YahooAuthenticationOptions GetYahoo(string key, string secret)
        {
            var tmp = new YahooAuthenticationOptions()
            {
                ConsumerSecret = secret,
                ConsumerKey = key,
                Provider = new YahooAuthenticationProvider()
                {
                    OnAuthenticated = OnAuthenticatedYahoo
                }
            };
            return tmp;
        }

        private static Task OnAuthenticatedYahoo(YahooAuthenticatedContext ctx)
        {
            //var expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var data = new
            {
                provider = "yahoo",
                id = ctx.UserId,
                username = ctx.NickName,
                email = ctx.Email,
                name = ctx.NickName,
                url = string.Format("https://yahoo.com/{0}", ctx.NickName),
                picture = string.Format("http://avatars.io/yahoo/{0}", ctx.NickName),
                verified = (bool)ctx.User["verified"],
                accesstoken = ctx.AccessToken,
                accesstokensecret = ctx.AccessTokenSecret,
                expiry = DateTime.Now.Add(new TimeSpan(1)),
                extra = ctx.User
            };
            ctx.Identity.AddClaim(new Claim("yahoo:about:" + ctx.UserId, data.ToJson()));
            return Task.FromResult(0);
        }

        public static DropboxAuthenticationOptions GetDropbox(string key, string secret)
        {
            var tmp = new DropboxAuthenticationOptions()
            {
                AppKey = key,
                AppSecret = secret,
                Provider = new DropboxAuthenticationProvider()
                {

                    OnAuthenticated = OnAuthenticatedDropbox
                }
            };
            return tmp;
        }

        private static Task OnAuthenticatedDropbox(DropboxAuthenticatedContext ctx)
        {
            //var expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var data = new
            {
                provider = "dropbox",
                id = ctx.Id,
                name = ctx.Name,
                url = string.Format("https://dropbox.com/{0}", ctx.Id),
                picture = string.Format("http://avatars.io/dropbox/{0}", ctx.Id),
                accesstoken = ctx.AccessToken,
                expiry = DateTime.Now.Add(new TimeSpan(1)),
                extra = ctx.User
            };
            ctx.Identity.AddClaim(new Claim("dropbox:about:" + ctx.Id, data.ToJson()));
            return Task.FromResult(0);
        }

        public static GitHubAuthenticationOptions GetGithub(string key, string secret)
        {
            var tmp = new GitHubAuthenticationOptions()
            {
                ClientId = key,
                ClientSecret = secret,
                Provider = new GitHubAuthenticationProvider()
                {
                    OnAuthenticated = OnAuthenticatedGitHub
                }
            };
            return tmp;
        }

        private static Task OnAuthenticatedGitHub(GitHubAuthenticatedContext ctx)
        {
            //var expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var data = new
            {
                provider = "github",
                id = ctx.Id,
                username = ctx.Id,
                email = ctx.Email,
                name = ctx.Name,
                url = string.Format("https://github.com/{0}", ctx.Id),
                picture = string.Format("http://avatars.io/github/{0}", ctx.Id),
                accesstoken = ctx.AccessToken,
                expiry = DateTime.Now.Add(new TimeSpan(1)),
                extra = ctx.User
            };
            ctx.Identity.AddClaim(new Claim("github:about:" + ctx.Id, data.ToJson()));
            return Task.FromResult(0);
        }

        public static SoundCloudAuthenticationOptions GetSoundCloud(string key, string secret)
        {
            var tmp = new SoundCloudAuthenticationOptions()
            {
                ClientId = key,
                ClientSecret = secret,
                Provider = new SoundCloudAuthenticationProvider()
                {
                    OnAuthenticated = OnAuthenticatedSoundCloud
                }
            };
            return tmp;
        }

        private static Task OnAuthenticatedSoundCloud(SoundCloudAuthenticatedContext ctx)
        {
            //var expiryDuration = ctx.ExpiresIn ?? new TimeSpan();
            var data = new
            {
                provider = "soundcloud",
                id = ctx.Id,
                username = ctx.UserName,
                email = ctx.UserName + "@soundcloud.com",
                name = ctx.UserName,
                url = string.Format("https://soundcloud.com/{0}", ctx.UserName),
                picture = string.Format("http://avatars.io/soundcloud/{0}", ctx.UserName),
                verified = (bool)ctx.User["verified"],
                accesstoken = ctx.AccessToken,
                expiry = DateTime.Now.Add(new TimeSpan(1)),
                extra = ctx.User
            };
            ctx.Identity.AddClaim(new Claim("soundcloud:about:" + ctx.Id, data.ToJson()));
            return Task.FromResult(0);
        }
    }
}