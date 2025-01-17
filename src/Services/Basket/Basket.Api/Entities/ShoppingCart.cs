﻿namespace Basket.Api.Entities;

public class ShoppingCart
{
    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; } = string.Empty;

    public List<ShopingCartItem> Items { get; set; } = new List<ShopingCartItem>();

    public decimal TotalPrice
    {
        get => Items.Sum(x => Math.Max(0, x.Price - x.DiscountAmount) * x.Quantity);
    }
}