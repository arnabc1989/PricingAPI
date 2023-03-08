using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCSV
{
    class Program
    {
        static void Main(string[] args)
        {

            var data = new List<Product>
            {
            new Product { StoreID = 1001, SKU = "ABC123", ProductName = "Pen", Price = 5, Date = DateTime.Today },
            new Product { StoreID = 1002, SKU = "DEF456", ProductName = "Marker", Price = 15, Date = DateTime.Today },
            new Product { StoreID = 1003, SKU = "GHI789", ProductName = "Book", Price = 80, Date = DateTime.Today },
             };


            string filename = @"D:\Projects\Assignments\PricingAPI\PricingAPI\Products.csv";
            string[] header = { "Store ID", "SKU", "Product Name", "Price", "Date" };


            using (var writer = new StreamWriter(filename))
            {
                writer.WriteLine(string.Join(",", header));
                foreach (var product in data)
                {
                    writer.WriteLine($"{product.StoreID},{product.SKU},{product.ProductName},{product.Price},{product.Date:yyyy-MM-dd}");
                }
            }
            
        }
    }

    class Product
    {
        public int StoreID { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
