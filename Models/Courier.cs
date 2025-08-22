namespace FoodDeliveryApp.Models;

public class Courier
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    
    // GPS // GPS // GPS // GPS // GPS // GPS
    public double Latitude { get; set; } = 0.0;
    public double Longitude { get; set; } = 0.0;
    // GPS // GPS // GPS // GPS // GPS // GPS
    
    public List<Order>? Orders { get; set; }
    public bool IsAvailable { get; set; } = true;
}