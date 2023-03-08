using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PricingAPI.Models;
using PricingAPI.Data.Repositories;
using PricingAPI.Data;

namespace PricingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricingController : Controller
    {
        private readonly IPersistPricingFeeds _pricingFeeds;
        public PricingController(IPersistPricingFeeds pricingFeeds)
        {
            _pricingFeeds = pricingFeeds;
        }
        // GET api/pricing?storeId=1&sku=ABC&productName=ProductA
        [HttpGet]
        public async Task<ActionResult<PricingFeed>> GetPricingRecords(int? storeId, string sku, string productName)
        {
            try 
            { 
                return await _pricingFeeds.GetPricingRecords(storeId, sku, productName); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/pricing/1
        [HttpGet("{id}")]
        public async Task<ActionResult<PricingFeed>> GetPricingRecordById(int id)
        {
            try
            {
                var pricingRecord = _pricingFeeds.GetPricingRecordById(id);
                if (pricingRecord == null)
                {
                    return NotFound();
                }

                return await pricingRecord;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("upload-pricing-feeds")]
        public async Task<IActionResult> UploadPricingFeeds(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File not selected");
                }

                if (!Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("Invalid file format");
                }

                var filePath = Path.GetTempFileName();

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var pricingFeeds = ReadFile.ReadCsvFile(filePath);
                _pricingFeeds.PersistPricingFeedsList(pricingFeeds);

                return Ok(pricingFeeds);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdatePricingRecord(int id, [FromBody] PricingFeed pricingFeed)
        {
            try
            {
                if (pricingFeed == null || pricingFeed.StoreId != id)
                {
                    return BadRequest();
                }

                bool success = await _pricingFeeds.UpdatePricingRecord(pricingFeed);

                if (success)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
