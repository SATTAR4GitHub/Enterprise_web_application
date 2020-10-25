using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fashionplex.Services;
using Fashionplex.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fashionplex.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly ICartService _cartService;
        public CheckoutController(ICheckoutService checkoutService, ICartService cartService)
        {
            _checkoutService = checkoutService;
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartItems"] = _cartService.GetCartItems();

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel checkoutViewModel)
        {
            _checkoutService.ProcessCheckout(checkoutViewModel);

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}