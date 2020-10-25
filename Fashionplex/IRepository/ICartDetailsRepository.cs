using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.IRepository
{
   public interface ICartDetailsRepository
    {
        CartDetail FindCartItemById(long id);
        IEnumerable<CartDetail> FindCartItemsByCartId(long cartId);
        void SaveCartItem(CartDetail cartItem);
        void UpdateCartItem(CartDetail cartItem);
        void DeleteCartItem(CartDetail cartItem);
    }
}
