using Discount.Grpc;
using Grpc.Core;
using static Discount.Grpc.DiscountService;

namespace Basket.Api.Services;

public class DiscountGrpcService
{
    private readonly DiscountServiceClient _discountServiceClient;

    public DiscountGrpcService(DiscountServiceClient discountServiceClient)
    {
        _discountServiceClient = discountServiceClient;
    }

    public async Task<Coupon?> GetDiscount(string productName)
    {
        try
        {
            var request = new GetDiscountRequest { ProductName = productName };
            var coupon = await _discountServiceClient.GetDiscountAsync(request);
            return coupon;
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            return null;
        }
    }
}