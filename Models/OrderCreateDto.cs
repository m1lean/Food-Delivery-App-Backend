namespace FoodDeliveryApp.Models;

public class OrderCreateDto
{
    public int UserId { get; set; }               // ID пользователя (в будущем можно брать из JWT)
    public int RestaurantId { get; set; }         // ID ресторана
    public List<int> ProductIds { get; set; } = new(); // список Id продуктов
}