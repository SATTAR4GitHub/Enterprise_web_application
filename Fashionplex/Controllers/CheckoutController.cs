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
    /// This class handles checkout process and order confirmation page.
    /// </summary>
    public class CheckoutController : Controller
    {
        /// <summary>
        /// Instance of Checkout service
        /// </summary>
        private readonly ICheckoutService _checkoutService;
        /// <summary>
        /// Instance of Cart service
        /// </summary>
        private readonly ICartService _cartService;

        /// <summary>
        /// Initialize check out service and cart service
        /// </summary>
        /// <param name="checkoutService"></param>
        /// <param name="cartService"></param>
        public CheckoutController(ICheckoutService checkoutService, ICartService cartService)
        {
            _checkoutService = checkoutService;
            _cartService = cartService;
        }

        /// <summary>
        /// Display and handle checkout form. 
        /// Also kep track of the cart total, total items, and item details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Checkout()
        {
            // Session variables to use later
            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartItems"] = _cartService.GetCartItems();

            return View();
        }

        /// <summary>
        /// This method gives an opprtunity to submit order and render the confirmation page.
        /// </summary>
        /// <param name="checkoutViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel checkoutViewModel)
        {
            _checkoutService.ProcessCheckout(checkoutViewModel);

            return RedirectToAction("Confirmation");
        }

        /// <summary>
        /// Helper method for confirmation page
        /// </summary>
        /// <returns></returns>
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}