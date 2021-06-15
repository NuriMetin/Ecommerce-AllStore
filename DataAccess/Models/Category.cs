using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; } 
    }
}
