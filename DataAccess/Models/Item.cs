﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SubcategoryId { get; set; }
        public int ProducerId { get; set; }
    }
}
