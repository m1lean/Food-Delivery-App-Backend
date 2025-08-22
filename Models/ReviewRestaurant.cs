using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Models
{
    public class ReviewRestaurant
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public required string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        // 🔗 Связи
        public int RestaurantId { get; set; }
        public required Restaurant Restaurant { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }
    }
}