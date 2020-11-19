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

    /// <summary>
    /// This class contains all the methods to complete CRUD (Create, read, update, delete) operation for the products.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _dbContext;

        /// <summary>
        /// Initialize DbContext
        /// </summary>
        /// <param name="dbContext"></param>
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add product to the database
        /// </summary>
        /// <param name="product"></param>
        public void CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Get product by product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(long id)
        {
            var product = _dbContext.Products.Find(id);
            return product;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts()
        {
            var products = _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand);
            return products;
        }

        /// <summary>
        /// Update products
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete products
        /// </summary>
        /// <param name="product"></param>
        public void DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }
    }
}
