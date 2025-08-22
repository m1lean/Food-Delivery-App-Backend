using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // ------------------- Рестораны -------------------
            if (!context.Restaurants.Any())
            {
                var restaurants = new List<Restaurant>
                {
                    new() { Name = "McDonald's", Latitude = 48.8566, Longitude = 2.3522 },
                    new() { Name = "Pizza Hut", Latitude = 41.9028, Longitude = 12.4964 },
                    new() { Name = "Burger King", Latitude = 52.5200, Longitude = 13.4050 },
                    new() { Name = "Sushi Samba", Latitude = 51.5074, Longitude = -0.1278 },
                    new() { Name = "Le Pain Quotidien", Latitude = 50.8503, Longitude = 4.3517 },
                    new() { Name = "La Casa del Gelato", Latitude = 45.4642, Longitude = 9.1900 }
                };

                context.Restaurants.AddRange(restaurants);
                context.SaveChanges();

                // ------------------- Продукты -------------------
                var products = new List<Product>
                {
                    new() { Name = "Big Mac", Price = 5.5m, Category = ProductCategory.Burgers, RestaurantId = 1, ImageUrl="https://linktobigmac.jpg" },
                    new() { Name = "Cheeseburger", Price = 4.0m, Category = ProductCategory.Burgers, RestaurantId = 1 },
                    new() { Name = "Pepperoni Pizza", Price = 8.0m, Category = ProductCategory.Pizza, RestaurantId = 2, Discount=0.1m },
                    new() { Name = "Margherita Pizza", Price = 7.5m, Category = ProductCategory.Pizza, RestaurantId = 2 },
                    new() { Name = "Whopper", Price = 6.0m, Category = ProductCategory.Burgers, RestaurantId = 3 },
                    new() { Name = "California Roll", Price = 12.0m, Category = ProductCategory.Sushi, RestaurantId = 4 },
                    new() { Name = "Matcha Cake", Price = 4.5m, Category = ProductCategory.Desserts, RestaurantId = 4 },
                    new() { Name = "Croissant", Price = 2.5m, Category = ProductCategory.Desserts, RestaurantId = 5 },
                    new() { Name = "Gelato", Price = 3.0m, Category = ProductCategory.Desserts, RestaurantId = 6 }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // ------------------- Курьеры -------------------
            if (!context.Couriers.Any())
            {
                var couriers = new List<Courier>
                {
                    new() { Name="Alice", Latitude=48.8566, Longitude=2.3522 },
                    new() { Name="Bob", Latitude=41.9028, Longitude=12.4964 },
                    new() { Name="Charlie", Latitude=52.5200, Longitude=13.4050 },
                    new() { Name="Diana", Latitude=51.5074, Longitude=-0.1278 },
                    new() { Name="Ethan", Latitude=50.8503, Longitude=4.3517 }
                };

                context.Couriers.AddRange(couriers);
                context.SaveChanges();
            }
        }
    }
}
