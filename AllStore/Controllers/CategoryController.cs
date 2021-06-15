using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Services.AdoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<Category>> AllCategories()
        {
            return await _categoryService.GetAllAsync();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromForm] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _categoryService.CreateAsync(category))
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromForm] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _categoryService.UpdateAsync(category))
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id == 0)
                return NotFound();

            if (!await _categoryService.DeleteAsync(id))
                return BadRequest();

            return Ok();
        }

    }
}
