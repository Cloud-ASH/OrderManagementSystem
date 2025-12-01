using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Entities;

namespace Order.Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationCore.Entities.Order> Orders { get; set; }
    public DbSet<Order_Details> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationCore.Entities.Order>(entity =>
        {
            entity.ToTable("Order");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Order_Date).IsRequired();
            entity.Property(e => e.CustomerId).IsRequired();
            entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PaymentMethodId).IsRequired();
            entity.Property(e => e.PaymentName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.ShippingAddress).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ShippingMethod).IsRequired().HasMaxLength(50);
            entity.Property(e => e.BillAmount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Order_Status).IsRequired().HasMaxLength(50);

            entity.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.Order_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Order_Details>(entity =>
        {
            entity.ToTable("Order_Details");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Order_Id).IsRequired();
            entity.Property(e => e.Product_Id).IsRequired();
            entity.Property(e => e.Product_name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Qty).IsRequired();
            entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Discount).HasColumnType("decimal(18,2)");
        });
    }
}
