using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AHY.WebApi
{
    internal static class JWTTokenGenerator
    {
        public static string GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Hakanhakannhakan.34"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(issuer: "http://localhost",claims:null ,audience: "http://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Member"));

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
           
            return handler.WriteToken(jwtSecurityToken);
        }
    }
}
