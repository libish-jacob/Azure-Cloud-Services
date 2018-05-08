Packages required.

Microsoft.Owin.Security.OpenIdConnect
Microsoft.Owin.Security.Cookies
Microsoft.Owin.Host.SystemWeb - required to use owin startup page in the root.


You need to have an application registered in your domain AD.
You have to have an [Authorize] attribute on your controller or your controller method which needs authorization.
provide domain hint in OpenIdConnectAuthenticationOptions


Now this will force login to domain AD using your domain account.


When working with localhost, make sure that you have the ssl enabled and local host url is registered in your AD app as a redirect url.


https://docs.microsoft.com/en-us/azure/app-service/app-service-mobile-how-to-configure-active-directory-authentication

