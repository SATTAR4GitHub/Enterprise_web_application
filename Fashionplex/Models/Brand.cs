using Fashionplex.Enums;
using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models 
{
    /// <summary>
    /// This class contains Brand's fetures with get and set methods, creates
    /// brand table via entity framework. 
    /// </summary>
    public class Brand : BaseModel
    {
        public string BrandName { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public BrandStatus BrandStatus { get; set; }
    }
}
