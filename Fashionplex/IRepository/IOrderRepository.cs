﻿using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.IRepository
{
   public interface IOrderRepository
    {
        Order FindOrderById(long id);
        IEnumerable<Order> GetAllOrders();
        void SaveOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
