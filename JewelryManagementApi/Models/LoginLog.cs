using System.ComponentModel.DataAnnotations;

namespace JewelryManagementApi.Models;

public class LoginLog
{
    [Key]
    public int LoginLogId { get; set; }

    [Required]
    [StringLength(200)]
    public string UserName { get; set; } = string.Empty;

    public DateTime LoginTime { get; set; }

    public DateTime? LastActive { get; set; }

    public bool IsSuccessful { get; set; }
}
