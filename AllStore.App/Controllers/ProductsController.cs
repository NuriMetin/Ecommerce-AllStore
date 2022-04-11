using AllStore.Domain.Abstractions.Services;
using AllStore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllStore.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int id)
        {
            return await _productService.GetProductsByCategoryIdAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Product>> GetProductsBySubcategoryId(int id)
        {
            return await _productService.GetProductsBySubcategoryIdAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProductByProductId(int id)
        {
            return await _productService.GetByIdAsync(id);
        }
    }
}
