namespace FoodDeliveryApp.Models;

public enum OrderStatus
{
    Pending,
    Preparing,
    OnTheWay,
    Completed,
    Cancelled
}

public enum Currency
{
    UAH,
    MDL,
    EUR,
    USD
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public int CourierId { get; set; }
    public Courier? Courier { get; set; }

    public List<CartItem> CartItems { get; set; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime StatusUpdatedAt { get; set; } = DateTime.UtcNow;
    public decimal DeliveryFee { get; set; } = 0.0m;
    public Currency Currency { get; set; } = Currency.UAH;
}