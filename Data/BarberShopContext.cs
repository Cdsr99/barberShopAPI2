using BarberShopAPI2.Models;

namespace BarberShopAPI2.Data;
using Microsoft.EntityFrameworkCore;

public class BarberShopContext: DbContext
{
    public BarberShopContext(DbContextOptions options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasIndex(b => b.SchedulesId)
            .IsUnique();
    }

    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Settings> Settings { get; set; }
}