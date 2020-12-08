using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains correspondence data with get and set methods, creates
    /// contact table via entity framework. 
    /// </summary>
    public class Contact
    {
        [Key]
        [Display(Name = "Contact ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
