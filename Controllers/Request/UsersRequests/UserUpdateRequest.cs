using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Controllers.Request.UsersRequests;

public record UserUpdateRequest(
    [Required] string UserName,
    string? Password,
    string? Email,
    string? Name,
    string? Profile,
    string? PhoneNumber
);