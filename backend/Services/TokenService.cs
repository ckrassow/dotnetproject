using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using EuroPredApi.Models;

namespace EuroPredApi.Services;

public class TokenService {

    private readonly IConfiguration _config;

    public TokenService(IConfiguration config) {

        _config = config;
    }

    public string GenerateToken(User user) {

        var securityKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {

            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };

        var token = new JwtSecurityToken(
            
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);  
    }
}