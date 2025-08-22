using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RestaurantsController(AppDbContext context)
    {
        _context = context;
    }

    // ------------------- Получение ресторанов с пагинацией и поиском -------------------
    [HttpGet]
    public async Task<ActionResult> GetRestaurants(
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Restaurants.Include(r => r.Products).AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(r => r.Name.Contains(search));

        var total = await query.CountAsync();
        var restaurants = await query
            .OrderBy(r => r.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new { total, page, pageSize, data = restaurants });
    }

    // ------------------- Создание ресторана -------------------
    [HttpPost]
    public async Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRestaurants), new { id = restaurant.Id }, restaurant);
    }
}