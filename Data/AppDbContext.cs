using Microsoft.EntityFrameworkCore;
using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Courier> Couriers => Set<Courier>();
    
    public DbSet<ReviewRestaurant> ReviewRestaurants { get; set; }
    public DbSet<ReviewCourier> ReviewCouriers { get; set; }

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
}