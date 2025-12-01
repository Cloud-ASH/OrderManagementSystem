using System.ComponentModel.DataAnnotations;

namespace Order.ApplicationCore.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Order_Date { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    public int PaymentMethodId { get; set; }

    [Required]
    [MaxLength(50)]
    public string PaymentName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string ShippingMethod { get; set; } = string.Empty;

    [Required]
    public decimal BillAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public string Order_Status { get; set; } = string.Empty;

    public ICollection<Order_Details> OrderDetails { get; set; } = new List<Order_Details>();
}
