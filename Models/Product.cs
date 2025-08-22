// Models/Product.cs
public enum ProductCategory
{
    Burgers,
    Pizza,
    Drinks,
    Desserts,
    Salads,
    Sandwiches,
    Sushi,
    Other
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
    public bool IsAvailable { get; set; } = true; // для меню в реальном времени
    public string? ImageUrl { get; set; } // ссылка на картинку продукта

    public decimal? Discount { get; set; } // процент скидки, например 0.1 = 10%

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;
}