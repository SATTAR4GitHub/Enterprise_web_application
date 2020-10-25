using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.IRepository
{
    public interface IOrderDetailsRepository
    {
        OrderDetails FindOrderItemById(long id);
        IEnumerable<OrderDetails> FindOrderItemByOrderId(long orderId);
        IEnumerable<OrderDetails> GetAllOrderItems();
        void SaveOrderItem(OrderDetails orderItem);
        void UpdateOrderItem(OrderDetails orderItem);
        void DeleteOrderItem(OrderDetails orderItem);
    }
}
