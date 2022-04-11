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
    public class MenuController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public MenuController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryService.GetAllAsync();
        }
    }
}
