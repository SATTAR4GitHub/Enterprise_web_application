using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains cart item's details information with get and set methods, creates
    /// cartDetail table via entity framework. 
    /// </summary>
    public class CartDetail : BaseModel
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public long CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
