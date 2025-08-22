using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // ------------------- Получение продуктов с фильтрацией и пагинацией -------------------
    [HttpGet]
    public async Task<ActionResult> GetProducts(
        [FromQuery] int? restaurantId = null,
        [FromQuery] ProductCategory? category = null,
        [FromQuery] bool? available = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Products
            .Include(p => p.Restaurant)
            .AsQueryable();

        if (restaurantId != null)
            query = query.Where(p => p.RestaurantId == restaurantId);
        if (category != null)
            query = query.Where(p => p.Category == category);
        if (available != null)
            query = query.Where(p => p.IsAvailable == available);

        var total = await query.CountAsync();
        var products = await query
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new
        {
            total,
            page,
            pageSize,
            data = products
        });
    }

    // ------------------- Создание продукта -------------------
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        // Проверяем, существует ли ресторан
        var restaurant = await _context.Restaurants.FindAsync(product.RestaurantId);
        if (restaurant == null)
            return BadRequest("Ресторан не найден");

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }

    // ------------------- Обновление доступности продукта -------------------
    [HttpPut("{id}/availability")]
    public async Task<IActionResult> UpdateAvailability(int id, [FromQuery] bool isAvailable)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound("Продукт не найден");

        product.IsAvailable = isAvailable;
        await _context.SaveChangesAsync();

        return Ok(product);
    }
}
