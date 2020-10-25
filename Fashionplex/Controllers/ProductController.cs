using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fashionplex.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fashionplex.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ProductController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public IActionResult Index(string category_slug = "all-categories", string brand_slug = "all-brands")
        {
            ViewData["SelectedCategory"] = category_slug;
            ViewData["SelectedBrand"] = brand_slug;

            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartDetails"] = _cartService.GetCartItems();

            var products = _productService.FetchProducts(category_slug, brand_slug);

           ViewData["Page"] = products.PagedProducts.CurrentPage;

            return View(products);
        }

        [HttpGet]
        public ActionResult AdvancedSearch()
        {
            

            return View("_AdvancedSearchPartial");
        }

    

}
}