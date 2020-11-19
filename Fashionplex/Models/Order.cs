using Fashionplex.Enums;
using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains order's information with get and set methods, creates
    /// order table via entity framework. 
    /// </summary>
    public class Order : BaseModel
    {
        [Display(Name = "Total Price")]
        public decimal OrderTotal { get; set; }
        [Display(Name = "Total Before Tax")]
        public decimal OrderItemTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public long ShipmentId { get; set; }
        [Display(Name ="Shipping Address")]
        public Shipment ShipmentInfo { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }
    }
}
