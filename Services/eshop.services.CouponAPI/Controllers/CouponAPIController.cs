using AutoMapper;
using eshop.services.CouponAPI.Data;
using eshop.services.CouponAPI.Models;
using eshop.services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eshop.services.CouponAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _res;
        IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _res = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                IEnumerable<Coupon> list = await _db.Coupons.ToListAsync();
                _res.Result = _mapper.Map<IEnumerable<CouponDto>>(list); ;
                _res.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _res.IsSuccess = false;
                _res.Message = ex.Message;
            }
            return Ok(_res);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Coupon? coupon = await _db.Coupons.FindAsync(id);
                _res.Result = _mapper.Map<CouponDto>(coupon); ;
                _res.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _res.Message = ex.Message;
                _res.IsSuccess = false;
            }
            return Ok(_res);
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                Coupon? coupon = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponCode == code);
                _res.Result = _mapper.Map<CouponDto>(coupon);
                _res.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _res.Message = ex.Message;
                _res.IsSuccess = false;
            }
            return Ok(_res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CouponDto coupon)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(coupon);
                _db.Coupons.Add(obj);
                int result = await _db.SaveChangesAsync();
                if (result == -1)
                    return BadRequest();
                else
                {
                    _res.IsSuccess = true;
                    _res.Result = result;
                }
            }
            catch (Exception ex)
            {
                _res.IsSuccess = false;
                _res.Message = ex.Message;
            }
            return Ok(_res);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CouponDto coupon)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(coupon);
                _db.Coupons.Update(obj);
                int result = await _db.SaveChangesAsync();
                if (result == -1)
                    return BadRequest();
                else
                {
                    _res.IsSuccess = true;
                    _res.Result = result;
                }
            }
            catch (Exception ex)
            {
                _res.IsSuccess = false;
                _res.Message = ex.Message;
            }
            return Ok(_res);
        }

        [HttpDelete]
        [Route("{id}")]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var coupon = await _db.Coupons.FirstAsync(x => x.CouponId == id);
                _db.Coupons.Remove(coupon);
                var saveResult = await _db.SaveChangesAsync();
                _res.IsSuccess = true;
                if (saveResult == -1)
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _res.Message = ex.Message;
                _res.IsSuccess = false;
            }
            return Ok(_res);
        }
    }
}
