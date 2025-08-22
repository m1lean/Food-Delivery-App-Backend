using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            .Include(o => o.Courier)
            .Include(o => o.User)
            .Include(o => o.CartItems)
            .ThenInclude(ci => ci.Product)
            .ToListAsync();
    }

    // ------------------- История заказов текущего пользователя -------------------
    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<Order>>> GetUserOrderHistory()
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub").Value);
        var orders = await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.CartItems)
            .ThenInclude(ci => ci.Product)
            .Include(o => o.Restaurant)
            .Include(o => o.Courier)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return Ok(orders);
    }

    // ------------------- Создание нового заказа -------------------
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderCreateDto dto)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub").Value);

        // Ищем продукты
        var productIds = dto.Items.Select(i => i.ProductId).ToList();
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .Include(p => p.Restaurant)
            .ToListAsync();

        if (!products.Any()) return BadRequest("Продукты не найдены");

        // Проверяем, чтобы все продукты из одного ресторана
        var restaurantIds = products.Select(p => p.RestaurantId).Distinct().ToList();
        if (restaurantIds.Count > 1) return BadRequest("Продукты из разных ресторанов запрещены");

        var restaurantId = restaurantIds.First();

        // Находим свободного курьера
        var courier = await _context.Couriers.FirstOrDefaultAsync(c => c.IsAvailable);
        if (courier == null) return BadRequest("Нет свободных курьеров");
        courier.IsAvailable = false;

        // Создаём CartItems
        var cartItems = dto.Items.Select(i =>
        {
            var product = products.First(p => p.Id == i.ProductId);
            return new CartItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = i.Quantity
            };
        }).ToList();

        // Пример фиксированной доставки (можно заменить динамической)
        decimal deliveryFee = 5.0m;

        var order = new Order
        {
            UserId = userId,
            RestaurantId = restaurantId,
            CourierId = courier.Id,
            CartItems = cartItems,
            DeliveryFee = deliveryFee,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            StatusUpdatedAt = DateTime.UtcNow
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

        if (order == null) return NotFound("Заказ не найден");

        order.Status = status;
        order.StatusUpdatedAt = DateTime.UtcNow;

        // Освобождаем курьера при завершении или отмене
        if (status == OrderStatus.Completed || status == OrderStatus.Cancelled)
        {
            if (order.Courier != null)
                order.Courier.IsAvailable = true;
        }

        await _context.SaveChangesAsync();
        return Ok(order);
    }
}
