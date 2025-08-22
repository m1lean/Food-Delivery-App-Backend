namespace FoodDeliveryApp.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public List<Product> Products { get; set; } = new();
    public List<ReviewRestaurant> Reviews { get; set; } = new();
}