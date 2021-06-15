using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ISubCategoryService : IRepository<SubCategory>
    {
        Task<IEnumerable<SubCategory>> GetSubcategoriesByCategoryIdAsync(int categoryId);
    }
}
