namespace FoodDeliveryApp.Models;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public int Quantity { get; set; } = 1;

    public int OrderId { get; set; }
    public Order? Order { get; set; }

}