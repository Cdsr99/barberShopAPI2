using Microsoft.AspNetCore.Identity;

namespace BarberShopAPI2.Models;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Profile { get; set; }
}