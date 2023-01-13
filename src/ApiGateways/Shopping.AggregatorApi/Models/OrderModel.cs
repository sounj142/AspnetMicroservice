namespace Shopping.AggregatorApi.Models;

public class OrderModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }

    // BillingAddress
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public OrderModelItem[] OrderItems { get; set; } = new OrderModelItem[0];
}

public class OrderModelItem
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }

    public string ProductName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? ImageFile { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountAmount { get; set; }
}