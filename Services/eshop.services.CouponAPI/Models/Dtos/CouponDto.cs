namespace eshop.services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string? CouponCode { get; set; }
        public float DiscountAmount { get; set; }
        public float MinAmount { get; set; }
    }
}
