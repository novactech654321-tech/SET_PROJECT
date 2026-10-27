using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelryManagementApi.Models;

public class Jewellery
{
    [Key]
    public int JewelId { get; set; }

    [Required]
    [StringLength(100)]
    public string JewelType { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int VendorId { get; set; }

    [StringLength(500)]
    public string? JewelPicture { get; set; }

    [ForeignKey(nameof(VendorId))]
    public Vendor? Vendor { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }

    public ICollection<VendorDeal>? Deals { get; set; }
}
