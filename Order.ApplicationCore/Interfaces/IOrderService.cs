using Order.ApplicationCore.Entities;

namespace Order.ApplicationCore.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Entities.Order>> GetAllOrdersAsync();
    Task<IEnumerable<Entities.Order>> GetOrdersByCustomerIdAsync(int customerId);
    Task<Entities.Order?> GetOrderByIdAsync(int orderId);
    Task<Entities.Order> CreateOrderAsync(Entities.Order order);
    Task<Entities.Order> UpdateOrderAsync(Entities.Order order);
    Task<bool> DeleteOrderAsync(int orderId);
}
