using Fashionplex.CustomeFields;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [PersonalData]
        [MaxLength(50)]
        public string LastName { get; set; }
        [PersonalData]
        [MaxLength(60)]
        public string AddressLine1 { get; set; }
        [PersonalData]
        [MaxLength(60)]
        public string AddressLine2 { get; set; }
        [PersonalData]
        [MaxLength(30)]
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
