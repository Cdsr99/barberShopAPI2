using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Controllers.Request;

public record ScheduleRequest([Required] DateTime date, [Required] string? hour);