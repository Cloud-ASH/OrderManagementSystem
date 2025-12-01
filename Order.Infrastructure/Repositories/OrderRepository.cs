using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Interfaces;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplicationCore.Entities.Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderDetails)
            .ToListAsync();
    }

    public async Task<IEnumerable<ApplicationCore.Entities.Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
            .Include(o => o.OrderDetails)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<ApplicationCore.Entities.Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<ApplicationCore.Entities.Order> AddOrderAsync(ApplicationCore.Entities.Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<ApplicationCore.Entities.Order> UpdateOrderAsync(ApplicationCore.Entities.Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
            return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }
}
