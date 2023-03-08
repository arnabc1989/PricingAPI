using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using PricingAPI.Context;
using PricingAPI.Models;


namespace PricingAPI.Data.Repositories
{
    public class PersistPricingFeeds : IPersistPricingFeeds
    {
        private readonly PricingFeedContext _dbContext;
        private readonly List<PricingFeed> _pricingRecords = new List<PricingFeed>
        {
            new PricingFeed { StoreId = 1001, SKU = "ABC123", ProductName = "Pen", Price = 5, Date = DateTime.Today },
            new PricingFeed { StoreId = 1002, SKU = "DEF456", ProductName = "Marker", Price = 15, Date = DateTime.Today },
            new PricingFeed { StoreId = 1003, SKU = "GHI789", ProductName = "Book", Price = 80, Date = DateTime.Today },
        };
        public PersistPricingFeeds(PricingFeedContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async void PersistPricingFeedsList(List<PricingFeed> pricingFeeds)
        {

            foreach (var pricingFeed in pricingFeeds)
            {
                var existingPricingFeed = _dbContext.PricingFeeds
                                            .FirstOrDefault(pf => pf.StoreId == pricingFeed.StoreId &&
                                                            pf.SKU == pricingFeed.SKU);

                if (existingPricingFeed != null)
                {
                    existingPricingFeed.ProductName = pricingFeed.ProductName;
                    existingPricingFeed.Price = pricingFeed.Price;
                    existingPricingFeed.Date = pricingFeed.Date;
                }
                else
                {
                    _dbContext.PricingFeeds.Add(pricingFeed);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PricingFeed> GetPricingRecords(int? storeId, string sku, string productName)
        {
            PricingFeed query = new PricingFeed();

            if (storeId.HasValue)
            {
                query = _pricingRecords.FirstOrDefault(pr => pr.StoreId == storeId.Value);
            }

            if (!string.IsNullOrEmpty(sku))
            {
                query = _pricingRecords.FirstOrDefault(pr => pr.SKU.Contains(sku));
            }

            if (!string.IsNullOrEmpty(productName))
            {
                query = _pricingRecords.FirstOrDefault(pr => pr.ProductName.Contains(productName));
            }

            return await Task.Run(() =>
            {
                return query;

            });
        }

        public async Task<PricingFeed> GetPricingRecordById(int id)
        {
            return await Task.Run(() =>
            {
                return _pricingRecords.FirstOrDefault(pr => pr.StoreId == id);
            });
        }

        public async Task<bool> UpdatePricingRecord(PricingFeed pricingRecord)
        {
            var existingRecord = _pricingRecords.FirstOrDefault(pr => pr.StoreId == pricingRecord.StoreId);
            if (existingRecord != null)
            {
                existingRecord.StoreId = pricingRecord.StoreId;
                existingRecord.SKU = pricingRecord.SKU;
                existingRecord.ProductName = pricingRecord.ProductName;
                existingRecord.Price = pricingRecord.Price;
                existingRecord.Date = pricingRecord.Date;
                return await Task.FromResult(true);
            }
            else { return await Task.FromResult(false); }

        }

    }
}
