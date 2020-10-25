using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    public class CartDetail : BaseModel
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public long CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
