// Models/OrderCreateDto.cs
public class OrderCreateDto
{
    public List<CartItemDto> Items { get; set; } = new();
}

public class CartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}