using AllStore.Models;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class MenuController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        public MenuController(ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }


        public async Task<string> GetAllCategories()
        {

            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            IEnumerable<SubCategory> subCategories = await _subCategoryService.GetAllAsync();

            List<MenuModel> menuModels = new List<MenuModel>();

            foreach (var category in categories)
            {
                MenuModel menuModel = new MenuModel();
                menuModel.CategoryId = category.ID;
                menuModel.CategoryName = category.Name;

                foreach (SubCategory subCategory in subCategories.Where(x => x.CategoryId == category.ID).ToList())
                {
                    menuModel.SubCategories.Add(subCategory);
                }

                menuModels.Add(menuModel);
            }
            var result =  JsonConvert.SerializeObject(menuModels);
            return result;
        }
    }
}
