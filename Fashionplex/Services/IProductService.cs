using Fashionplex.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Services
{
    /// <summary>
    /// Product interface that forces the Product Service to implement the declared method.
    /// </summary>
    public interface IProductService
    {
        ProductViewModel FetchProducts(string categorySlug, string brandSlug);
    }
}
