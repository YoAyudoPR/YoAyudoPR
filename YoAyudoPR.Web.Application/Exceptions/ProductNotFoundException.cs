using System;
namespace YoAyudoPR.Web.Application.Exceptions
{
	public class ProductNotFoundException : Exception
	{
        public ProductNotFoundException(string? parent, string? region, string? url)
        : base($"Product not found | Parent: {parent} | Region: {region} | Url: {url}")
        {
            Parent = parent;
            Region = region;
            Url = url;
        }

        public ProductNotFoundException(string? parent, string? region, string? url, Exception innerException)
            : base($"Product not found | Parent: {parent} | Region: {region} | Url: {url}", innerException)
        {
            Parent = parent;
            Region = region;
            Url = url;
        }

        public ProductNotFoundException(int? productId, string? couponCode)
        : base($"Product not found | ProductId: {productId} | CouponCode: {couponCode}")
        {
            ProductId = productId;
            CouponCode = couponCode;
        }

        public ProductNotFoundException(int? productId, string? couponCode, Exception innerException)
        : base($"Product not found | ProductId: {productId} | CouponCode: {couponCode}", innerException)
        {
            ProductId = productId;
            CouponCode = couponCode;
        }
        
        public int? ProductId { get; set; }
        public string? CouponCode { get; set; }
        public string? Parent { get; set; }
        public string? Region { get; set; }
        public string? Url { get; set; }
    }
}

