using Microsoft.AspNetCore.Mvc;
using Order.ApplicationCore.Interfaces;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    // GET: api/order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationCore.Entities.Order>>> GetAllOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting orders");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/order/customer/{customerId}
    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<ApplicationCore.Entities.Order>>> GetOrdersByCustomerId(int customerId)
    {
        try
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            if (!orders.Any())
                return NotFound($"No orders found for customer {customerId}");
            
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting orders for customer {CustomerId}", customerId);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/order/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationCore.Entities.Order>> GetOrderById(int id)
    {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            
            return Ok(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting order {OrderId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/order
    [HttpPost]
    public async Task<ActionResult<ApplicationCore.Entities.Order>> CreateOrder([FromBody] ApplicationCore.Entities.Order order)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/order/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<ApplicationCore.Entities.Order>> UpdateOrder(int id, [FromBody] ApplicationCore.Entities.Order order)
    {
        try
        {
            if (id != order.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _orderService.GetOrderByIdAsync(id);
            if (existing == null)
                return NotFound();

            var updated = await _orderService.UpdateOrderAsync(order);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating order {OrderId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/order/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        try
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result)
                return NotFound();
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting order {OrderId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
