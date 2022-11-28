using Discount.grpc.Entities;

namespace Discount.grpc.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string ProductName);

        //Task<bool> HasDiscount(string ProductName);

        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string ProductName);
        
    }
}
