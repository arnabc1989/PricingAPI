using Microsoft.EntityFrameworkCore;
using PricingAPI.Models;


namespace PricingAPI.Data
{
    public class PricingFeedContext : DbContext
    {
        public PricingFeedContext(DbContextOptions<PricingFeedContext> options) : base(options)
        {
        }

        public DbSet<PricingFeed> PricingFeeds { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{            
        //    modelBuilder.Entity<PricingFeed>().ToTable("PricingFeed");
        //}
    }
}
