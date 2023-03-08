using CsvHelper;
using PricingAPI.Models;

namespace PricingAPI.Mapper
{
    public sealed class PricingFeedMap : CsvHelper.Configuration.ClassMap<PricingFeed>
    {
        public PricingFeedMap()
        {
            Map(m => m.StoreId).Index(0);
            Map(m => m.SKU).Index(1);
            Map(m => m.ProductName).Index(2);
            Map(m => m.Price).Index(3);
            Map(m => m.Date).Index(4);
        }

    }
}
