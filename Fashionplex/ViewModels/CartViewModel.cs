using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.ViewModels
{
    public class CartViewModel
    {
        public long ProductId { get; set; }

        public string CategorySlug { get; set; }
        public string BrandSlug { get; set; }
        public string Page { get; set; }
    }
}
