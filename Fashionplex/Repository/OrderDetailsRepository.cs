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
    /// This class contains all the methods to complete CRUD (Create, read, update, delete) operation for the order details table.
    /// </summary>
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Initialize DbContext
        /// </summary>
        /// <param name="context"></param>
        public OrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderDetails FindOrderItemById(long id)
        {
            var orderDetails = _context.OrderDetails.Find(id);
            return orderDetails;
        }

        public IEnumerable<OrderDetails> FindOrderItemByOrderId(long orderId)
        {
            var orderItems = _context.OrderDetails.Where(o => o.OrderId == orderId);
            return orderItems;
        }

        public IEnumerable<OrderDetails> GetAllOrderItems()
        {
            var orderItems = _context.OrderDetails;
            return orderItems;
        }

        public void SaveOrderItem(OrderDetails orderDetails)
        {
            _context.OrderDetails.Add(orderDetails);
            _context.SaveChanges();
        }

        public void UpdateOrderItem(OrderDetails orderDetails)
        {
            _context.OrderDetails.Update(orderDetails);
            _context.SaveChanges();
        }

        public void DeleteOrderItem(OrderDetails orderDetails)
        {
            _context.OrderDetails.Remove(orderDetails);
            _context.SaveChanges();
        }
    }
}
