using Discount.grpc.Protos;

namespace Basket.API.DiscountService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public async Task<CouponModel> getDiscount(string productName)
        {
            var discount = new RequestToGetDiscount();
            discount.ProductName = productName;

            return await _grpcClient.getDiscountAsync(discount);
        }
    }
}
