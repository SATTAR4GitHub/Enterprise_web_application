﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Models.Shared
{
    /// <summary>
    /// Base model class with some common fetures of the application model classes
    /// </summary>
    public class BaseModel
    {
        public long Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Modified")]
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
