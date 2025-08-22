using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Models
{
    public class ReviewCourier
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public required string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        // 🔗 Связи
        public int CourierId { get; set; }
        public required Courier Courier { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }
    }
}