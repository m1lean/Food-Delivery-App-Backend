using Microsoft.AspNetCore.Mvc;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Services;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        // ⚠️ пока пароль сохраняем как есть (для теста), позже сделаем Hash
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] User login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.PasswordHash == login.PasswordHash);
        if (user == null) return Unauthorized("Invalid credentials");

        var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email);
        return Ok(new { token });
    }
}