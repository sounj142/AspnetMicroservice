﻿namespace Basket.Api.Models;

public class BasketCheckout
{
    public string UserName { get; set; } = string.Empty;

    // BillingAddress
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
}