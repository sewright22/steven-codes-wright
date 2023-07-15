using System.Security.Claims;

namespace WebApi.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.GetClaimValue("UserId");
        }

        public static string GetClaimValue(this ClaimsPrincipal principal, string claimType)
        {
            Claim? claim = principal.Claims.FirstOrDefault(x => x.Type == claimType);

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return claim.Value;
        }
    }
}
