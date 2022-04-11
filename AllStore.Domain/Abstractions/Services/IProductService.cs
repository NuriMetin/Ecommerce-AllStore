using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Domain.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId);
        Task<Product> GetByIdAsync(int id);
    }
}
