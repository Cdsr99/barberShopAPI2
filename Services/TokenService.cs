using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BarberShopAPI2.Models;

namespace BarberShopAPI2.Services;

public class TokenService: ITokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User usuario)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id),
            new Claim("normalizedUserName", usuario.NormalizedUserName),
            new Claim("profile", usuario.Profile),
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