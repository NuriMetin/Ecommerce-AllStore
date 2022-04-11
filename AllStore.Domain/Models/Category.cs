using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllStore.Domain.Models
{
    public class Category
    {
        public Category()
        {
            SubCategories = new List<SubCategory>();
        }
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }
}
