using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.ApplicationCore.Entities;

public class Order_Details
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Order_Id { get; set; }

    [Required]
    public int Product_Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Product_name { get; set; } = string.Empty;

    [Required]
    public int Qty { get; set; }

    [Required]
    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    [ForeignKey("Order_Id")]
    public Order Order { get; set; } = null!;
}
