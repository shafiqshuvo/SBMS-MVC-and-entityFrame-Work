﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileError.Model.Model
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public int Id { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public List<Product> Products { get; set; }
    }
}
