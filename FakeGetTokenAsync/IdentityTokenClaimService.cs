using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FakeGetTokenAsync;

public class IdentityTokenClaimService
{
    public async Task<string> FakeGetTokenAsync(string userName, string registered)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("G3VF4C6KFV43JH6GKCDFGJH45V36JHGV3H4C6F3GJC63HG45GH6V345GHHJ4623FJL3HCVMO1P23PZ07W8");
        var issuer = "arbems.com";
        var audience = "Public";
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

        claims.Add(new Claim("UserType", registered));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}