using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BarberShopAPI2.Models;
using Microsoft.IdentityModel.Tokens;

namespace BarberShopAPI2.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User usuario)
    {
        Claim[] claims =
        {
            new("username", usuario.UserName),
            new("id", usuario.Id),
            new("normalizedUserName", usuario.NormalizedUserName),
            new("profile", usuario.Profile)
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}