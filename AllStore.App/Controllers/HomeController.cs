using AllStore.Domain.Abstractions.Services;
using AllStore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllStore.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("{id}")]
        public async Task<string> Products(int id)
        {
            IEnumerable<Product> products = await _productService.GetProductsByCategoryIdAsync(id);
            string result = JsonConvert.SerializeObject(products);

            return result;
        }
    }
}
