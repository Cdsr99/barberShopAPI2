using BarberShopAPI2.Models;

namespace BarberShopAPI2.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}