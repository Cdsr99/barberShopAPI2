using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Controllers.Request;

public record ScheduleDayBetweenRequest([Required] DateTime startDay, [Required] DateTime endDay);