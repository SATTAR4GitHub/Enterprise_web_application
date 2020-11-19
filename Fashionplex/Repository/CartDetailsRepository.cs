using Fashionplex.Data;
using Fashionplex.IRepository;
using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Repository
{
    /// <summary>
    /// This class contains all the methods to complete CRUD (Create, read, update, delete) operation for the shopping cart items.
    /// </summary>
    public class CartDetailsRepository : ICartDetailsRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Initialize DbContext
        /// </summary>
        /// <param name="context"></param>
        public CartDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CartDetail FindCartItemById(long id)
        {
            var cartDetails = _context.CartDetails.Find(id);

            return cartDetails;
        }

        public IEnumerable<CartDetail> FindCartItemsByCartId(long cartId)
        {
            var cartDetails = _context.CartDetails.Where(c => c.CartId == cartId);
            return cartDetails;
        }

        public void SaveCartItem(CartDetail cartDetails)
        {
            _context.CartDetails.Add(cartDetails);
            _context.SaveChanges();
        }

        public void UpdateCartItem(CartDetail cartDetails)
        {
            _context.CartDetails.Update(cartDetails);
            _context.SaveChanges();
        }

        public void DeleteCartItem(CartDetail cartDetails)
        {
            _context.CartDetails.Remove(cartDetails);
            _context.SaveChanges();
        }
    }
}
