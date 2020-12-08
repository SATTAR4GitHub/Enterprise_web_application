using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fashionplex.Services;
using Fashionplex.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fashionplex.Controllers
{
    /// <summary>
    /// This class handles the items to be added to the shopping cart while a user is
    /// browsing your site. These items are retrieved and displayed in a standard “shopping cart” format, 
    /// allowing the user to update the quantity or remove items from the cart.
    /// This controller works with cart services and cart repository to populate data as a 2nd layer. 
    /// For details, please look at Services/CartService.cs class.
    /// </summary>
    public class CartController : Controller
    {
        /// <summary>
        /// Instance of the CartService interface  
        /// </summary>
        private readonly ICartService _cartService;

        /// <summary>
        /// Initialize Services/CartService
        /// </summary>
        /// <param name="cartService"></param>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Method that render and display cart details page. Also keep track of the cart total, total items, and item details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CartDetail()
        {
            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartDetails"] = _cartService.GetCartItems();

            return View();
        }

        /// <summary>
        /// Method to delete the cart item
        /// </summary>
        /// <param name="deleteFromCartViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveCartItem(DeleteFromCartViewModel deleteFromCartViewModel)
        {
            // This method is called from CartService class where all the logics are provided
            _cartService.RemoveFromCart(deleteFromCartViewModel);

            return RedirectToAction("CartDetail");
        }

        /// <summary>
        /// Method to add to the cart when user click the button "Add to Cart" from the product page
        /// </summary>
        /// <param name="cartViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddItemToCart(CartViewModel cartViewModel)
        {
            // Details for this method are in the ./Services/CartService.cs
            _cartService.AddToCart(cartViewModel);


            if (cartViewModel.CategorySlug == "all-categories" && cartViewModel.BrandSlug == "all-brands")
            {
                return RedirectToAction("Index", "Product", new { category_slug = "", brand_slug = "", page = cartViewModel.Page == "1" ? "" : cartViewModel.Page });
            }
            else
            {
                return RedirectToAction("Index", "Product", new { category_slug = cartViewModel.CategorySlug, brand_slug = cartViewModel.BrandSlug, page = cartViewModel.Page == "1" ? null : cartViewModel.Page });
            }

        }
    }
}