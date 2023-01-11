using MediatR;
using Ordering.Application.Dtos;

namespace Ordering.Application.Features.Orders.Commands;

public class CheckoutOrderCommand : IRequest<int>
{
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

    public OrderItemDto[] OrderItems { get; set; } = new OrderItemDto[0];
}