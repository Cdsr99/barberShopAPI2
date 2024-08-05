using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Controllers.Request.UsersRequests;

public record UserCreateRequest(
    [Required] string UserName,
    [Required] string Name,
    [Required] string Email,
    [Required] string Password,
    [Required] string Profile);