using BarberShopAPI2.Models;

namespace BarberShopAPI2.Data;
using Microsoft.EntityFrameworkCore;

public class BarberShopContext: DbContext
{
    public BarberShopContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Settings> Settings { get; set; }
}