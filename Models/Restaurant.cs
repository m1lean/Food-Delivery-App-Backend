using System.Collections.Generic;
using FoodDeliveryApp.Models; // ✅ добавляем пространство имён для ReviewRestaurant

namespace FoodDeliveryApp.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public required string Name { get; set; } = null!;
        public required string Description { get; set; }
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;

        public List<ReviewRestaurant> Reviews { get; set; } = new();
        public List<Product>? Products { get; set; }
    }
}