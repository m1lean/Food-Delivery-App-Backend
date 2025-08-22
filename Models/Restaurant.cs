namespace FoodDeliveryApp.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    
    // GPS // GPS // GPS // GPS // GPS // GPS
    public double Latitude { get; set; } = 0.0;
    public double Longitude { get; set; } = 0.0;
    // GPS // GPS // GPS // GPS // GPS // GPS
    public List<Product> Products { get; set; } = new();
}