using System.ComponentModel.DataAnnotations;

namespace JewelryManagementApi.Models;

public class Benefit
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string BenefitName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? BenefitDescription { get; set; }

    public DateTime? BenefitLastDate { get; set; }
}
