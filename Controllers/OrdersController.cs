using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodDeliveryApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    // ------------------- Получение всех заказов -------------------
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.Products)
            .Include(o => o.Courier)
            .Include(o => o.User)
            .ToListAsync();
    }

    // ------------------- Создание нового заказа -------------------
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderCreateDto dto)
    {
        // Ищем ресторан
        var restaurant = await _context.Restaurants.FindAsync(dto.RestaurantId);
        if (restaurant == null)
            return BadRequest("Ресторан не найден");

        // Ищем продукты
        var products = await _context.Products
            .Where(p => dto.ProductIds.Contains(p.Id))
            .ToListAsync();

        if (!products.Any())
            return BadRequest("Продукты не найдены");

        // Ищем свободного курьера
        var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.IsAvailable);
        if (courier == null)
            return BadRequest("Нет свободных курьеров");

        courier.IsAvailable = false; // курьер занят

        var order = new Order
        {
            UserId = dto.UserId,
            RestaurantId = dto.RestaurantId,
            Products = products,
            CourierId = courier.Id,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    // ------------------- Обновление статуса заказа -------------------
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromQuery] OrderStatus status)
    {
        var order = await _context.Orders
            .Include(o => o.Courier)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return NotFound("Заказ не найден");

        order.Status = status;

        // Если заказ завершён или отменён, освобождаем курьера
        if (status == OrderStatus.Completed || status == OrderStatus.Cancelled)
        {
            if (order.Courier != null)
                order.Courier.IsAvailable = true;
        }

        await _context.SaveChangesAsync();
        return Ok(order);
    }
}
