using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fashionplex.Data;
using Fashionplex.Models;
using Fashionplex.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fashionplex.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ApplicationDbContext _context;

        public ProductController(IProductService productService, ICartService cartService, ApplicationDbContext context)
        {
            _productService = productService;
            _cartService = cartService;
            _context = context;
        }

        public IActionResult Index(string searchString, string category_slug = "all-categories", string brand_slug = "all-brands")
        {
            ViewData["SelectedCategory"] = category_slug;
            ViewData["SelectedBrand"] = brand_slug;

            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartDetails"] = _cartService.GetCartItems();

            var productsVM = _productService.FetchProducts(category_slug, brand_slug);

            ViewData["Page"] = productsVM.PagedProducts.CurrentPage;

            // Filter parameter
            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                var filteredProducts = (from prod in productsVM.PagedProducts.Products
                                        where (prod.ProductName.Contains(searchString))
                                        select prod).ToList();
                productsVM.PagedProducts.Products = filteredProducts;
            }

            return View(productsVM);
        }

        [HttpGet]
        public ActionResult AdvancedSearch()
        {
            return View("_AdvancedSearchPartial");
        }



    }
}