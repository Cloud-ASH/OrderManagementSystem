using Order.ApplicationCore.Interfaces;

namespace Order.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<ApplicationCore.Entities.Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersAsync();
    }

    public async Task<IEnumerable<ApplicationCore.Entities.Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
    }

    public async Task<ApplicationCore.Entities.Order?> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetOrderByIdAsync(orderId);
    }

    public async Task<ApplicationCore.Entities.Order> CreateOrderAsync(ApplicationCore.Entities.Order order)
    {
        if (order.Order_Date == default)
            order.Order_Date = DateTime.Now;
        return await _orderRepository.AddOrderAsync(order);
    }

    public async Task<ApplicationCore.Entities.Order> UpdateOrderAsync(ApplicationCore.Entities.Order order)
    {
        return await _orderRepository.UpdateOrderAsync(order);
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        return await _orderRepository.DeleteOrderAsync(orderId);
    }
}
