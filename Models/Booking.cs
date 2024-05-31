namespace BarberShopAPI2.Models;

public class Booking
{
    public int Id { get; set; }
    public string Client { get; set; }
    public string ClientsPhone { get; set; }
    public int SchedulesId { get; set; }
    public string Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}