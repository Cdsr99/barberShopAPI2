using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Controllers.Request.BooksRequest;

public record BooksCreateRequest([Required] string clientName, [Required] string clientsPhoneNumber, [Required] int schedulesId ); 