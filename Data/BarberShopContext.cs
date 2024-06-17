using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BarberShopAPI2.Data;
using Microsoft.EntityFrameworkCore;

public class BarberShopContext: IdentityDbContext<User>
{
    public BarberShopContext(DbContextOptions<BarberShopContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasIndex(b => b.SchedulesId)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasIndex(b => b.Email)
            .IsUnique();
        
        
        modelBuilder.Entity<User>()
            .HasIndex(b => b.NormalizedEmail)
            .IsUnique();
        
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }

    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Settings> Settings { get; set; }

}