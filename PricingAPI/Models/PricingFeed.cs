using System;

namespace PricingAPI.Models
{
    public class PricingFeed
    {
        public int StoreId { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
