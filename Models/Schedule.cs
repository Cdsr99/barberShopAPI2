namespace BarberShopAPI2.Models;

public class Schedule
{
    public Schedule(DateTime dateTime, int hour)
    {
        Date = dateTime;
        Hour = hour;
    }

    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Hour { get; set; }
    public string? Status { get; set; }
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }
}