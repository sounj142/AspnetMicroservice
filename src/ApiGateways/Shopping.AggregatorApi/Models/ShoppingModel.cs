namespace Shopping.AggregatorApi.Models;

public class ShoppingModel
{
    public string UserName { get; set; } = string.Empty;
    public ShopingCartModel? Basket { get; set; }
    public OrderModel[]? Orders { get; set; }
}