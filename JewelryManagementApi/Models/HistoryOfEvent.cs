using System.ComponentModel.DataAnnotations;

namespace JewelryManagementApi.Models;

public class HistoryOfEvent
{
    [Key]
    public int EventId { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    public DateTime Time { get; set; }
}
