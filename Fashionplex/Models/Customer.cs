using Fashionplex.CustomeFields;
using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains customer's fetures with get and set methods, creates
    /// customer table via entity framework. 
    /// </summary>
    public class Customer : BaseModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z.]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z.]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(60)]
        public string Email { get; set; }
        [MaxLength(20)]
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
