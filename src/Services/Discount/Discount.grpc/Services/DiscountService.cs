using AutoMapper;
using Discount.grpc.Entities;
using Discount.grpc.Protos;
using Discount.grpc.Repositories.Interfaces;
using Grpc.Core;

namespace Discount.grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(ILogger<DiscountService> logger,
                                IDiscountRepository repository,
                                IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public override async Task<CouponModel> getDiscount(RequestToGetDiscount request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"There is no Discount for {request.ProductName}"));
                _logger.LogInformation($"Discount for {request.ProductName} does not exist");

            }
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;

        }

        public override async Task<CouponModel> createDiscount(RequestToCreateDiscount request, ServerCallContext context)
        {
            //shabby programming style
            var coupon = new Coupon
            {
                Description = request.Coupon.Description,
                Amount = request.Coupon.Amount,
                ProductName = request.Coupon.ProductName,
                Id = request.Coupon.Id

            };

            await _repository.CreateDiscount(coupon);

            _logger.LogInformation($"Discount{coupon.ProductName} Successfully created");
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;

        }

        public override async Task<CouponModel> updateDiscount(RequestToUpdateDiscount request, ServerCallContext context)
        {
            //very shabby programming
            var coupon = new Coupon
            {
                Description = request.Coupon.Description,
                Amount = request.Coupon.Amount,
                ProductName = request.Coupon.ProductName,
                Id = request.Coupon.Id
            };
            await _repository.UpdateDiscount(coupon);
            _logger.LogInformation($"Discount for {coupon.ProductName} Updated Successfully");

            var updated = _mapper.Map<CouponModel>(coupon);
            return updated;
        }


        public override async Task<DiscountResponse> deleteDiscount(RequestToDeleteDiscount request, ServerCallContext context)
        {
            var deleted =await _repository.DeleteDiscount(request.ProductName);
            var respond = new DiscountResponse
            {
                Response = deleted
            };
            
            _logger.LogInformation($"{request.ProductName} Deleted successfully");
            return respond;

        }
    }
}
