using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    public class ImageModel
    {

        public int Id { get; set; }
        [Display(Name = "Image Uploader")]
        [Required]
        public List<IFormFile> Photos { get; set; }
        public string PhotoUrl { get; set; }
    }
}
