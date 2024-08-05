using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShopAPI2.Models;

[Table("schedules")]
public class Schedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public DateTime Date { get; set; }

    public string? Hour { get; set; }

    public string? Status { get; set; } = "Available";

    [Column("created_at")] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.Now;
}