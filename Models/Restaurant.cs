namespace FoodDeliveryApp.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public List<Product> Products { get; set; } = new();
}