using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Domain.Abstractions.Repositories
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<List<SubCategory>> GetSubcategoriesByCategoryIdAsync(int categoryId);
    }
}
