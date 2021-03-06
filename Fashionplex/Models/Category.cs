﻿using Fashionplex.Enums;
using Fashionplex.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models
{
    /// <summary>
    /// This class contains product's category information with get and set methods, creates
    /// category table via entity framework. 
    /// </summary>
    public class Category : BaseModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public CategoryStatus CategoryStatus { get; set; }
    }
}
