using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllStore.Models
{
    public class MenuModel
    {
        public MenuModel()
        {
            SubCategories = new List<SubCategory>();
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategory> SubCategories;
    }
}
