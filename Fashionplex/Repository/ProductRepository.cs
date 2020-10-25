using Fashionplex.Data;
using Fashionplex.IRepository;
using Fashionplex.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public Product GetProductById(long id)
        {
            var product = _dbContext.Products.Find(id);
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand);
            return products;
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }
    }
}
