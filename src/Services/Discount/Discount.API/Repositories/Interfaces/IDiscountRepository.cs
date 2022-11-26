using Discount.API.Entities;

namespace Discount.API.Repositories.Interfaces
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
