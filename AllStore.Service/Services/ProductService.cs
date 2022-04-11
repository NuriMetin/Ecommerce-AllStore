using AllStore.Dal.AdoNet;
using AllStore.Domain.Abstractions.Repositories;
using AllStore.Domain.Abstractions.Services;
using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);  
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            return await _productRepository.GetProductsBySubcategoryIdAsync(subcategoryId);
        }
    }
}
