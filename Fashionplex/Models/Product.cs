using Fashionplex.Enums;
using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains product's fetures with get and set methods, creates
    /// product table via entity framework. 
    /// </summary>
    public class Product : BaseModel
    {
        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        public string ProductName { get; set; }
        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(150)]
        public string Description { get; set; }
        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        public string Slug { get; set; }
        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        public string MetaDescription { get; set; }
        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        public string MetaKeywords { get; set; }
        public string SKU { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public decimal OldPrice { get; set; }
        public string ImageUrl { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsBestseller { get; set; }
        public bool IsFeatured { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public long BrandId { get; set; }
        public Brand Brand { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
