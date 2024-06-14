using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Controllers.Request.UsersRequests;

public record UserLoginRequest([Required] string UserName, [Required] string Password);

