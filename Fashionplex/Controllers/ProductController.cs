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
    /// <summary>
    /// This class handles the products to be displayed in the store. This controller works with product services
    /// and product repository to populate data. For details, please look at Services/ProductService.cs class.
    /// </summary>
    public class ProductController : Controller
    {
        /// <summary>
        /// Product service instance
        /// </summary>
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initialize product service, cart service, and DbContext
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="cartService"></param>
        /// <param name="context"></param>
        public ProductController(IProductService productService, ICartService cartService, ApplicationDbContext context)
        {
            _productService = productService;
            _cartService = cartService;
            _context = context;
        }

        /// <summary>
        /// Method to display products submitted by the admin. Store data to variables and provide them to the view. 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="category_slug"></param>
        /// <param name="brand_slug"></param>
        /// <returns></returns>
        public IActionResult Index(string searchString, string category_slug = "all-categories", string brand_slug = "all-brands")
        {
            // session variables to user later in the views
            ViewData["SelectedCategory"] = category_slug;
            ViewData["SelectedBrand"] = brand_slug;

            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartDetails"] = _cartService.GetCartItems();

            // Populate all the products
            var productsVM = _productService.FetchProducts(category_slug, brand_slug);

            // Keep track of the page
            ViewData["Page"] = productsVM.PagedProducts.CurrentPage;

            // Filter parameter
            ViewData["CurrentFilter"] = searchString;

            // Filter data as per search string 
            if (!String.IsNullOrEmpty(searchString))
            {
                var filteredProducts = (from prod in productsVM.PagedProducts.Products
                                        where (prod.ProductName.Contains(searchString))
                                        select prod).ToList();
                productsVM.PagedProducts.Products = filteredProducts;
            }

            return View(productsVM);
        }

    }
}