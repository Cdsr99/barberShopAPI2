namespace BarberShopAPI2.Models;

public class Booking
{
    public Booking(string client, string clientsPhone, int schedulesId)
    {
        Client = client;
        ClientsPhone = clientsPhone;
        SchedulesId = schedulesId;
    }

    public int Id { get; set; }
    public string Client { get; set; }
    public string ClientsPhone { get; set; }
    public int SchedulesId { get; set; }
    public string Status { get; set; } = "Booked";
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}