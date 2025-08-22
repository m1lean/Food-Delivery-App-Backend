namespace FoodDeliveryApp.Models;

public class Courier
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsAvailable { get; set; } = true;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public List<Order> Orders { get; set; } = new();
    public List<ReviewCourier> Reviews { get; set; } = new();
}