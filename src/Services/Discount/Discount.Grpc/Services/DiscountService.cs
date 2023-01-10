using Discount.Grpc.Repositories;
using Grpc.Core;
using static Discount.Grpc.DiscountService;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountServiceBase
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountService(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public override async Task<Coupon> GetDiscount(
        GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);
        return coupon != null
            ? coupon
            : throw new RpcException(new Status(StatusCode.NotFound, "Discount not found."));
    }

    public override async Task<Coupon> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var result = await _discountRepository.CreateDiscount(new Coupon
        {
            ProductName = request.ProductName,
            Amount = request.Amount,
            Description = request.Description
        });
        return result;
    }

    public override async Task<UpdateDiscountResponse> UpdateDiscount(Coupon request, ServerCallContext context)
    {
        var result = await _discountRepository.UpdateDiscount(new Coupon
        {
            Id = request.Id,
            ProductName = request.ProductName,
            Amount = request.Amount,
            Description = request.Description
        });
        return new UpdateDiscountResponse
        {
            Result = result
        };
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var result = await _discountRepository.DeleteDiscount(request.ProductName);

        return new DeleteDiscountResponse
        {
            Result = result
        };
    }
}