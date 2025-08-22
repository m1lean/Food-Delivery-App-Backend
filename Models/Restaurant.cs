// Models/Restaurant.cs
public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public double Latitude { get; set; } = 0.0;
    public double Longitude { get; set; } = 0.0;

    public List<Product>? Products { get; set; }
}