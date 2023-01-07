namespace Basket.Api.Entities;

public class ShopingCartItem
{
    public int Quantity { get; set; }
    public string? Color { get; set; }
    public string ProductId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? ImageFile { get; set; }
    public decimal Price { get; set; }
}