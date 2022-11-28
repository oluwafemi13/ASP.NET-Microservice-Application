using Discount.grpc.Protos;
using Discount.grpc.Repositories.Interfaces;
using Grpc.Core;

namespace Discount.grpc.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(ILogger<DiscountService> logger,
                                IDiscountRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<CouponModel> getDiscount(RequestToGetDiscount request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null) {
                throw new RpcException(new Status(StatusCode.NotFound, $"There is no Discount for {request.ProductName}"));
                _logger.LogInformation($"Discount for {request.ProductName} does not exist");

            }
            return

        }
    }
}
