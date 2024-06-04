using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BarberShopAPI2.Models;

[Table("schedules")]
public class Schedule
{
    public Schedule(DateTime date, string hour)
    {
        Date = date;
        Hour = hour;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Hour { get; set; }

    [Required] public string Status { get; set; } = "Available";

    [Column("created_at")] public DateTime? CreatedAt { get; set; } = DateTime.Now;
    
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    
}