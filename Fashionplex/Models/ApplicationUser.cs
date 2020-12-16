using Fashionplex.CustomeFields;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains user's information and updates view after changes
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [RegularExpression(@"^[A-Z]+[a-zA-Z.]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [PersonalData]
        [RegularExpression(@"^[A-Z]+[a-zA-Z.]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [PersonalData]
        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(80)]
        public string AddressLine1 { get; set; }
        [PersonalData]
        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(80)]
        public string AddressLine2 { get; set; }
        [PersonalData]
        [RegularExpression(@"^[A-Z]+[a-zA-Z.]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        public string Country { get; set; }

        [MinimumAge(18, ErrorMessage = "You must be 18 years old.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }


        public ApplicationUser()
        {
            Messages = new HashSet<Message>();
        }

        // 1 - many relationship between ApplicationUSer and Message. One user has many messages.
        public virtual ICollection<Message> Messages { get; set; }
    }
}
