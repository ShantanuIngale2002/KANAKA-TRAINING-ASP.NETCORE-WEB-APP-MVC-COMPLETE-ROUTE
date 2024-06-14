using GenericRepoPatternCORE.Models;

namespace GenericRepoPatternCORE.Repository
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public Product PostProduct(Product product);
    }
}
