using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BarberShopAPI2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShopAPI2.Models;

[Table("schedules")]
public class Schedule
{
    public Schedule() { }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(255)]
    public string Hour { get; set; }

    [Required]
    [MaxLength(255)]
    public string Status { get; set; }
    
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    
}