using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicAPI.Helper
{
    public class AuthOptions
    {
        public const string ISSUER = "MusicAPI";
        public const string AUDIENCE = "MusicApp";
        const string KEY = "aJhfrfz3oWo45HNaZH70UkhNM4M3taDi3xI93X99gDcn6Jf0jsNlC4UngO70OwDm";
        public static SymmetricSecurityKey SymmetricSecurityKey { get { return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY)); } }

        public static string GenerateToken(int userId)
        {
            List<Claim> claims = new List<Claim>() { new Claim("id", userId.ToString()) };
            JwtSecurityToken jwt = new JwtSecurityToken
            (
                issuer: ISSUER,
                audience: AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwt);
        }
    }
}
