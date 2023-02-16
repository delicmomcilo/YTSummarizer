using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YTSummarizer.Auth.Security
{
    public class Security : ISecurity
    {

        public string? GetUserIdFromAccessToken(HttpRequest req)
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = req.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var userId = tokenS?.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            return userId;
        }
    }
}