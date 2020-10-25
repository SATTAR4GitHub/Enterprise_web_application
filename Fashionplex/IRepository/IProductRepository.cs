using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.IRepository
{
   public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product GetProductById(long id);
        IEnumerable<Product> GetAllProducts();
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
