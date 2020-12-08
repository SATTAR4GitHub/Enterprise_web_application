using Fashionplex.Enums;
using Fashionplex.IRepository;
using Fashionplex.Models;
using Fashionplex.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Services
{
    /// <summary>
    /// This class contains all the logics and methods to populate shopping cart using product repository, 
    /// and cart repository.
    /// </summary>
    public class CartService : ICartService
    {
        private const string UniqueCartIdSessionKey = "UniqueCartId";
        private readonly HttpContext _httpContext;
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailsRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initialize repositories
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="cartRepository"></param>
        /// <param name="cartItemRepository"></param>
        /// <param name="productRepository"></param>
        public CartService(IHttpContextAccessor httpContextAccessor,
                            ICartRepository cartRepository,
                            ICartDetailsRepository cartItemRepository,
                            IProductRepository productRepository)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Generarte a unique cart Guid id
        /// </summary>
        /// <returns></returns>
        public string UniqueCartId()
        {
            if (!string.IsNullOrWhiteSpace(_httpContext.Session.GetString(UniqueCartIdSessionKey)))
            {
                return _httpContext.Session.GetString(UniqueCartIdSessionKey);
            }
            else
            {
                var uniqueId = Guid.NewGuid().ToString();
                _httpContext.Session.SetString(UniqueCartIdSessionKey, uniqueId);

                return _httpContext.Session.GetString(UniqueCartIdSessionKey);
            }
        }

        /// <summary>
        /// Method to get all carts
        /// </summary>
        /// <returns></returns>
        public Cart GetCart()
        {
            var cart = _cartRepository.GetAllCarts().FirstOrDefault();
            return cart;
        }

        /// <summary>
        /// Method to add or update products to the shopping cart.
        /// </summary>
        /// <param name="cartViewModel"></param>
        public void AddToCart(CartViewModel cartViewModel)
        {
            var cart = GetCart();

            if (cart != null)
            {
                var existingCartItem = _cartItemRepository.FindCartItemsByCartId(cart.Id)
                                            .FirstOrDefault(c => c.ProductId == cartViewModel.ProductId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                    _cartItemRepository.UpdateCartItem(existingCartItem);
                }
                else
                {
                    var product = _productRepository.GetProductById(cartViewModel.ProductId);

                    if (product != null)
                    {
                        var cartItem = new CartDetail
                        {
                            CartId = cart.Id,
                            Cart = cart,
                            CreateDate = DateTime.Now,
                            ProductId = cartViewModel.ProductId,
                            Product = product,
                            Quantity = 1
                        };
                        _cartItemRepository.SaveCartItem(cartItem);
                    }
                }
            }
            else
            {
                var product = _productRepository.GetProductById(cartViewModel.ProductId);
                if (product != null)
                {
                    var newCart = new Cart
                    {
                        UniqueCartId = UniqueCartId(),
                        CreateDate = DateTime.Now,
                        CartStatus = CartStatus.Unchecked
                    };

                    _cartRepository.SaveCart(newCart);

                    var cartItem = new CartDetail
                    {
                        CartId = newCart.Id,
                        Cart = newCart,
                        CreateDate = DateTime.Now,
                        ProductId = cartViewModel.ProductId,
                        Product = product,
                        Quantity = 1
                    };

                    _cartItemRepository.SaveCartItem(cartItem);

                }
            }
        }

        /// <summary>
        /// Remove shopping cart item
        /// </summary>
        /// <param name="removeFromCartViewModel"></param>
        public void RemoveFromCart(DeleteFromCartViewModel removeFromCartViewModel)
        {
            var cartItem = _cartItemRepository.FindCartItemById(removeFromCartViewModel.CartItemId);
            _cartItemRepository.DeleteCartItem(cartItem);
        }

        /// <summary>
        /// Get a list of cart item details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CartDetail> GetCartItems()
        {
            IList<CartDetail> cartDetails = new List<CartDetail>();

            var cart = GetCart(); //This cart value always becomes null. That's the problem

            if (cart != null)
            {
                cartDetails = _cartItemRepository.FindCartItemsByCartId(cart.Id).ToArray();// played with static id//
            }

            return cartDetails;
        }

        /// <summary>
        /// Count total items in the cart
        /// </summary>
        /// <returns></returns>
        public int CartItemsCount()
        {

            var count = 0;
            var cartDetails = GetCartItems();

            foreach (var cartItem in cartDetails)
            {
                count += cartItem.Quantity;
            }

            return count;
        }

        /// <summary>
        /// Calculate shopping cart total amount
        /// </summary>
        /// <returns></returns>
        public decimal GetCartTotal()
        {
            decimal total = 0;

            var cartDetails = GetCartItems();

            foreach (var cartItem in cartDetails)
            {
                var product = _productRepository.GetProductById(cartItem.ProductId);
                total += cartItem.Quantity * product.Price;
            }

            return total;
        }
    }
}
