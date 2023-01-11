namespace Ordering.Application.Dtos;

public class OrderItemDto
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