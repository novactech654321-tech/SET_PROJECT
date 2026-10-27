using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelryManagementApi.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public int JewelId { get; set; }

    [Required]
    [StringLength(200)]
    public string Product { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Required]
    [StringLength(100)]
    public string TransactionType { get; set; } = string.Empty;

    public DateTime DateOfTransaction { get; set; }

    [ForeignKey(nameof(JewelId))]
    public Jewellery? Jewellery { get; set; }
}
