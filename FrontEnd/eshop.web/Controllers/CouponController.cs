using eshop.Frontend.Web.Models;
using eshop.Frontend.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace eshop.Frontend.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoupons()
        {
            List<CouponDto> list = new();

            ResponseDto response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                Console.WriteLine("error");
            }

            return Ok(list);
        }


        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _couponService.CreateCouponsAsync(model);

                if (response != null && response.IsSuccess)
                {
                    Console.WriteLine("Coupon created successfully");
                }
                else
                {
                    Console.WriteLine(response?.Message);
                }
            }
            return Ok(model);
        }
        [HttpDelete("CouponDeleteById")]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto response = await _couponService.DeleteCouponsAsync(couponId);

            if (response != null && response.IsSuccess)
            {
                CouponDto model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return Ok(model);
            }
            else
            {
                Console.WriteLine(response?.Message);
            }
            return NotFound();
        }

        [HttpDelete("CouponDelete")]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto response = await _couponService.DeleteCouponsAsync(couponDto.CouponId);

            if (response != null && response.IsSuccess)
            {
                Console.WriteLine("Coupon deleted successfully");
            }
            else
            {
                Console.WriteLine(response?.Message);
            }
            return Ok(couponDto);
        }

    }
}
