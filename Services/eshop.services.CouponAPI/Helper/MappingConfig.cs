
using AutoMapper;
using eshop.services.CouponAPI.Models;
using eshop.services.CouponAPI.Models.Dto;

namespace eshop.services.CouponAPI.Helper
{
    // public class MapperConfig : Profile
    // {
    //     public MapperConfig()
    //     {
    //         CreateMap<Coupon, CouponDto>();
    //     }
    // }

    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>();
                config.CreateMap<CouponDto, Coupon>();
            });
            return mappingConfig;
        }
    }
}