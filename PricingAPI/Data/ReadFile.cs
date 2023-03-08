using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper;
using PricingAPI.Models;
using PricingAPI.Mapper;

namespace PricingAPI.Data
{
    public class ReadFile
    {
        public static List<PricingFeed> ReadCsvFile(string filePath)
        {
            var pricingFeeds = new List<PricingFeed>();
           
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<PricingFeedMap>();

                    pricingFeeds = csv.GetRecords<PricingFeed>().ToList();
                }
            }
            return pricingFeeds;
        }
    }
}
