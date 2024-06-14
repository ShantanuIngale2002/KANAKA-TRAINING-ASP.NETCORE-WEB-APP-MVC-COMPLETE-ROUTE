using GenericRepoPatternCORE.Models;
using Microsoft.EntityFrameworkCore;

namespace GenericRepoPatternCORE.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
