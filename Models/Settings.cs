using System.ComponentModel.DataAnnotations;

namespace BarberShopAPI2.Models;

public class Settings
{
    [Key] public int Id { get; set; }

    [Required] public string Parameter { get; set; }

    public string Value { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}