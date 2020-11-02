using Fashionplex.CustomeFields;
using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    public class Customer : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        [MinimumAge(18, ErrorMessage = "You must be 18 years old.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Shipment> Shipments { get; set; }
    }
}
