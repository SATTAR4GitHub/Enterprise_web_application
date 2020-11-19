using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains chat messages information/fetures with get and set methods, creates
    /// message table via entity framework. 
    /// </summary>
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public string UserFullName { get; set; }

        public Message()
        {
            When = DateTime.Now;
        }
    }
}
