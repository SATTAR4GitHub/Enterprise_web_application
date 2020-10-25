using Fashionplex.Enums;
using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    public class Order : BaseModel
    {
        public decimal OrderTotal { get; set; }
        public decimal OrderItemTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public long ShipmentId { get; set; }
        public Shipment ShipmentInfo { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
