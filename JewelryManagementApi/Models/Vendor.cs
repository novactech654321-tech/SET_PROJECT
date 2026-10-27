using System.ComponentModel.DataAnnotations;

namespace JewelryManagementApi.Models;

public class Vendor
{
    [Key]
    public int VendorId { get; set; }

    [Required]
    [StringLength(200)]
    public string VendorName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? VendorDescription { get; set; }

    public ICollection<Jewellery>? Jewelleries { get; set; }

    public ICollection<VendorDeal>? Deals { get; set; }
}
