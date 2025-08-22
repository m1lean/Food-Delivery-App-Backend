using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using System.Security.Claims;

namespace FoodDeliveryApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        // -------------------- Отзывы о ресторанах --------------------

        [HttpPost("restaurant/{restaurantId}")]
        public async Task<IActionResult> AddRestaurantReview(int restaurantId, [FromBody] ReviewRestaurant review)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub").Value);

            if (!await _context.Restaurants.AnyAsync(r => r.Id == restaurantId))
                return NotFound("Ресторан не найден");

            review.RestaurantId = restaurantId;
            review.UserId = userId;
            review.CreatedAt = DateTime.UtcNow;

            _context.ReviewRestaurants.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<ReviewRestaurant>>> GetRestaurantReviews(int restaurantId)
        {
            if (!await _context.Restaurants.AnyAsync(r => r.Id == restaurantId))
                return NotFound("Ресторан не найден");

            var reviews = await _context.ReviewRestaurants
                .Where(r => r.RestaurantId == restaurantId)
                .Include(r => r.User)
                .ToListAsync();

            var avgRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            return Ok(new { AverageRating = avgRating, Reviews = reviews });
        }

        // -------------------- Отзывы о курьерах --------------------

        [HttpPost("courier/{courierId}")]
        public async Task<IActionResult> AddCourierReview(int courierId, [FromBody] ReviewCourier review)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub").Value);

            if (!await _context.Couriers.AnyAsync(c => c.Id == courierId))
                return NotFound("Курьер не найден");

            review.CourierId = courierId;
            review.UserId = userId;
            review.CreatedAt = DateTime.UtcNow;

            _context.ReviewCouriers.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

        [HttpGet("courier/{courierId}")]
        public async Task<ActionResult<IEnumerable<ReviewCourier>>> GetCourierReviews(int courierId)
        {
            if (!await _context.Couriers.AnyAsync(c => c.Id == courierId))
                return NotFound("Курьер не найден");

            var reviews = await _context.ReviewCouriers
                .Where(r => r.CourierId == courierId)
                .Include(r => r.User)
                .ToListAsync();

            var avgRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            return Ok(new { AverageRating = avgRating, Reviews = reviews });
        }
    }
}
