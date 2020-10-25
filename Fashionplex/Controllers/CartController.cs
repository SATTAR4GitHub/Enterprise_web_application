using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fashionplex.Services;
using Fashionplex.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fashionplex.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult CartDetail()
        {
            ViewData["CartTotal"] = _cartService.GetCartTotal();
            ViewData["CartItemsCount"] = _cartService.CartItemsCount();
            ViewData["CartDetails"] = _cartService.GetCartItems();

            return View();
        }

        [HttpPost]
        public IActionResult RemoveCartItem(DeleteFromCartViewModel deleteFromCartViewModel)
        {
            _cartService.RemoveFromCart(deleteFromCartViewModel);
            return RedirectToAction("CartDetail");
        }

        [HttpPost]
        public IActionResult AddItemToCart(CartViewModel cartViewModel)
        {
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