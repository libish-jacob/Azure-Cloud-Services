using System;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace WebApplication.Models
{
    public class UserIdentity
    {
        private const string RequiredDomain = "domain to login";
        private const string AdDomainName = @"Domain"; // the doamin

        private const string UsernameClaimV1 = ClaimsIdentity.DefaultNameClaimType;
        private const string UsernameClaimV2 = "preferred_username";

        public static string GetUsername()
        {
            return GetUsername(ClaimsPrincipal.Current);
        }

        public static string GetUsername(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var username = principal.FindFirst(UsernameClaimV2)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                // Fallback to Azure AD v1.0 claims
                username = principal.FindFirst(UsernameClaimV1)?.Value;
                if (string.IsNullOrEmpty(username))
                {
                    return null;
                }
            }

            var usernameMatch = Regex.Match(username, $"^(.+)@{RequiredDomain}$");
            if (!usernameMatch.Success)
            {
                return null;
            }

            return $@"{AdDomainName}\{usernameMatch.Groups[1]}";
        }
    }
}