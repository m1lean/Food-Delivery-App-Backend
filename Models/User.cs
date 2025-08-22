namespace FoodDeliveryApp.Models;

public enum UserRole
{
    Admin,
    Courier,
    Customer
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.Customer;

    public List<Order> Orders { get; set; } = new();
    public List<ReviewRestaurant> RestaurantReviews { get; set; } = new();
    public List<ReviewCourier> CourierReviews { get; set; } = new();
    public List<RefreshToken> RefreshTokens { get; set; } = new();
}