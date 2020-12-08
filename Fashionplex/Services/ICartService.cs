using Fashionplex.Models;
using Fashionplex.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Services
{
    /// <summary>
    /// Cart interface that forces the Cart Service to implement the declared methods.
    /// </summary>
    public interface ICartService
    {
        string UniqueCartId();
        Cart GetCart();
        void AddToCart(CartViewModel cartViewModel);
        void RemoveFromCart(DeleteFromCartViewModel removeFromCartViewModel);
        IEnumerable<CartDetail> GetCartItems();
        int CartItemsCount();
        decimal GetCartTotal();
    }
}
