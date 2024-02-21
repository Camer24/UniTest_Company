using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    public static class JwtHelper
    {
        public static string GetClaimValue(string token, string claim)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string authHeader = token.Replace("Bearer ", "").Replace("bearer ", "");
            JwtSecurityToken tokens = handler.ReadToken(authHeader) as JwtSecurityToken;

            Claim claimData = tokens.Claims.FirstOrDefault(cl => cl.Type.ToUpper() == claim.ToUpper());

            if(claimData == null || string.IsNullOrEmpty(claimData.Value))
                throw new UnauthorizedAccessException();

            return claimData.Value;
        }
    }
}
