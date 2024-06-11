using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Controllers.Request;

public record ScheduleDayRequest([Required] DateTime day);