using Fashionplex.Enums;
using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains Cart's fetures with get and set methods, creates
    /// cart table via entity framework. 
    /// </summary>
    public class Cart : BaseModel
    {
        public Cart()
        {
            CartDetails = new List<CartDetail>();
        }

        public string UniqueCartId { get; set; }
        public CartStatus CartStatus { get; set; }
        public IEnumerable<CartDetail> CartDetails { get; set; }
    }
}
