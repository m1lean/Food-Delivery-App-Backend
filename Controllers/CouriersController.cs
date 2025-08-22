using Microsoft.AspNetCore.Mvc;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouriersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CouriersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Courier>>> GetCouriers()
    {
        return await _context.Couriers.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Courier>> CreateCourier(Courier courier)
    {
        _context.Couriers.Add(courier);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCouriers), new { id = courier.Id }, courier);
    }
}