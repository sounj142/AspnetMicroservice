namespace Shopping.AggregatorApi.Models;

public class ShopingCartModel
{
    public string UserName { get; set; } = string.Empty;

    public List<ShopingCartModelItem> Items { get; set; } = new List<ShopingCartModelItem>();

    public decimal TotalPrice { get; set; }
}

public class ShopingCartModelItem
{
    public int Quantity { get; set; }
    public string? Color { get; set; }
    public string ProductId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? ImageFile { get; set; }
    public decimal Price { get; set; }
    public int DiscountAmount { get; set; }

    public string? Description { get; set; }
}