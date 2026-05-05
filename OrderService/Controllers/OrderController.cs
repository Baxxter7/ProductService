using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderContext _orderContext;
    public OrderController(OrderContext orderContext) {
        _orderContext = orderContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders() 
        =>  await _orderContext.Orders.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        _orderContext.Orders.Add(order);
        await _orderContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
    }
}
