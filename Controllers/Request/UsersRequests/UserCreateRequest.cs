using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Controllers.Request.UsersRequests;

public record UserCreateRequest([Required] string userName, [Required] string name, [Required] string email, [Required] string password, [Required] string profile);