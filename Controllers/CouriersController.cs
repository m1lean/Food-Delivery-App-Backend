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
public class CourierController : ControllerBase
{
    private readonly AppDbContext _context;

    public CourierController(AppDbContext context)
    {
        _context = context;
    }

    // Получение новых заказов для курьера
    [HttpGet("new-orders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetNewOrders()
    {
        var courierId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub").Value);

        var orders = await _context.Orders
            .Where(o => o.CourierId == courierId && o.Status == OrderStatus.Pending)
            .Include(o => o.CartItems)
            .ThenInclude(ci => ci.Product)
            .Include(o => o.Restaurant)
            .ToListAsync();

        return Ok(orders);
    }

    // Обновление координат курьера
    [HttpPut("update-location")]
    public async Task<IActionResult> UpdateLocation([FromQuery] double lat, [FromQuery] double lng)
    {
        var courierId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub").Value);
        var courier = await _context.Couriers.FindAsync(courierId);
        if (courier == null) return NotFound("Курьер не найден");

        courier.Latitude = lat;
        courier.Longitude = lng;
        await _context.SaveChangesAsync();
        return Ok(courier);
    }
}