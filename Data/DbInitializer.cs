using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Restaurants.Any()) return; // уже есть данные → не заполняем

        // --- Рестораны ---
        var mcDonalds = new Restaurant { Name = "McDonald's", Address = "str. Ovidiu, 3" };
        var kfc = new Restaurant { Name = "KFC", Address = "str. Ismail, 25" };
        var doncezar = new Restaurant { Name = "Domino's Pizza", Address = "str. Cuflia, 5" };

        context.Restaurants.AddRange(mcDonalds, kfc, doncezar);
        context.SaveChanges();

        // --- Продукты ---
        var products = new List<Product>
        {
            new() { Name = "Биг Мак", Price = 5.99m, RestaurantId = mcDonalds.Id },
            new() { Name = "Чизбургер", Price = 2.49m, RestaurantId = mcDonalds.Id },
            new() { Name = "Филадельфия Ролл", Price = 4.99m, RestaurantId = doncezar.Id },
            new() { Name = "Маргарита", Price = 7.99m, RestaurantId = doncezar.Id },
            new() { Name = "Крылышки", Price = 6.49m, RestaurantId = kfc.Id },
            new() { Name = "Баскет Комбо", Price = 9.99m, RestaurantId = kfc.Id }
        };
        context.Products.AddRange(products);

        // --- Курьеры ---
        var couriers = new List<Courier>
        {
            new() { Name = "Иван Петров", Phone = "+37360000001", IsAvailable = true },
            new() { Name = "Алексей Смирнов", Phone = "+37360000002", IsAvailable = true },
            new() { Name = "Мария Коваленко", Phone = "+37360000003", IsAvailable = true }
        };
        context.Couriers.AddRange(couriers);

        context.SaveChanges();
    }
}