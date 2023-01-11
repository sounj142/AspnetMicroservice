using MediatR;

namespace Ordering.Application.Features.Orders.Commands;

public class UpdateOrderCommand : IRequest
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

    // Payment
    public string? StripeId { get; set; }
}