using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains order's details information with get and set methods, creates
    /// orderDetails table via entity framework. 
    /// </summary>
    public class OrderDetails : BaseModel
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
