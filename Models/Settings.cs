namespace BarberShopAPI2.Models;

public class Settings
{
    public int Id { get; set; }
    public string? Parameter { get; set; }
    public string? Value { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}