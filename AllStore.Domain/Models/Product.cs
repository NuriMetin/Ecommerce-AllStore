using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Domain.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SubcategoryName { get; set; }
        public string ProducerName { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Image { get; set; }
    }
}
