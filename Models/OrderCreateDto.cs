namespace FoodDeliveryApp.Models;

public class OrderCreateDto
{
    public int RestaurantId { get; set; }           // ID ресторана
    public List<int> ProductIds { get; set; } = new(); // Список ID продуктов
}