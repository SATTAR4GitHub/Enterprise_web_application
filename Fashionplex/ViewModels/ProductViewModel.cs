using Fashionplex.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.ViewModels
{
    public class ProductViewModel
    {
        public PaginationViewModel PagedProducts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }

        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }

        public SelectList CategoryList { get; set; }
        public SelectList BrandList { get; set; }
    }
}
