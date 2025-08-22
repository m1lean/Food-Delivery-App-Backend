namespace FoodDeliveryApp.Models;

public enum OrderStatus
{
    Pending,     // Новый заказ
    Preparing,   // Готовится
    OnTheWay,    // В пути
    Completed,   // Доставлен
    Cancelled    // Отменен
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public int CourierId { get; set; }
    public Courier? Courier { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<Product> Products { get; set; } = new();
}