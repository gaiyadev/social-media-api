using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SocialMediaApp.Services;

public sealed class JwtService
{
    private readonly string _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException();
    private readonly string _jwtIssuer = Environment.GetEnvironmentVariable("JWT_SECRET_ISSUER") ?? throw new InvalidOperationException();
    private readonly string _jwtAudience = Environment.GetEnvironmentVariable("JWT_SECRET_AUDIENCE") ?? throw new InvalidOperationException();

    public string CreateToken(string email, string firstName, int id)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            // issuer: _jwtIssuer,
            // audience: _jwtAudience,
            claims: new[] {
                new Claim("email", email),
                new Claim("firstName", firstName),
                new Claim("id", id.ToString()),
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    } 
}