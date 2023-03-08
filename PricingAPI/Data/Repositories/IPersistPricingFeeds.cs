using PricingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PricingAPI.Data.Repositories
{
    public interface IPersistPricingFeeds
    {
        Task<PricingFeed> GetPricingRecords(int? storeId, string sku, string productName);
        Task<PricingFeed> GetPricingRecordById(int id);
        Task<bool> UpdatePricingRecord(PricingFeed pricingRecord);
        void PersistPricingFeedsList(List<PricingFeed> pricingFeeds);
    }
}
