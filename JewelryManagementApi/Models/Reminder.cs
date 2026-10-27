using System.ComponentModel.DataAnnotations;

namespace JewelryManagementApi.Models;

public class Reminder
{
    [Key]
    public int ReminderId { get; set; }

    [Required]
    [StringLength(200)]
    public string UserName { get; set; } = string.Empty;

    public DateTime DateSet { get; set; }

    public DateTime DateToRemind { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = string.Empty;
}
