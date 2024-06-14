using GenericRepoPatternCORE.Models;
using GenericRepoPatternCORE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepoPatternCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts() 
        { 
            return productService.GetProducts(); 
        }

        [HttpPost]
        public Product PostProduct(Product product)
        {
            return productService.PostProduct(product);
        }
    }
}
