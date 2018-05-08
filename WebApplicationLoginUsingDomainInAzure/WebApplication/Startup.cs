
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Configuration;
using System.Threading.Tasks;
using WebApplication;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApplication
{
    public class Startup
    {
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        private readonly string clientId = ConfigurationManager.AppSettings["ClientId"];

        // RedirectUri is the URL where the user will be redirected to after they sign in.
        private readonly string redirectUri = ConfigurationManager.AppSettings["RedirectUri"];

        // Tenant is the tenant ID (e.g. contoso.onmicrosoft.com, or 'common' for multi-tenant)
        private readonly string tenant = ConfigurationManager.AppSettings["Tenant"];

        // Authority is the URL for authority, composed by Azure Active Directory endpoint and the tenant name (e.g. https://login.microsoftonline.com/contoso.onmicrosoft.com)
        private readonly string authority = ConfigurationManager.AppSettings["Authority"];

        // DomainHint is to have users immediately land on the sign-in page for their organization
        private readonly string domainHint = ConfigurationManager.AppSettings["DomainHint"];


        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = string.Format(authority, tenant),
                    RedirectUri = redirectUri,
                    Scope = OpenIdConnectScope.OpenIdProfile,

                    ResponseType = OpenIdConnectResponseType.IdToken,
                    TokenValidationParameters = new TokenValidationParameters { ValidateIssuer = true },

                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        RedirectToIdentityProvider = context =>
                        {
                            context.ProtocolMessage.DomainHint = domainHint;
                            return Task.FromResult(0);
                        }
                    }
                }
            );
        }
    }
}
