using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelryManagementApi.Models;

public class VendorDeal
{
    [Key]
    public int VendorDealId { get; set; }

    public int VendorId { get; set; }

    public int JewelId { get; set; }

    [Required]
    [StringLength(200)]
    public string VendorName { get; set; } = string.Empty;

    [StringLength(100)]
    public string JewelType { get; set; } = string.Empty;

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public DateTime DateOfDeal { get; set; }

    public int CreditDuration { get; set; }

    public DateTime? PaymentDate { get; set; }

    [ForeignKey(nameof(VendorId))]
    public Vendor? Vendor { get; set; }

    [ForeignKey(nameof(JewelId))]
    public Jewellery? Jewellery { get; set; }
}
