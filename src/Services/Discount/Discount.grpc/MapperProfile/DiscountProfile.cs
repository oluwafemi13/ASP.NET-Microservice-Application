using AutoMapper;
using Discount.grpc.Entities;
using Discount.grpc.Protos;

namespace Discount.grpc.MapperProfile
{
    public class DiscountProfile :Profile
    {
        public DiscountProfile()
        {
             CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
