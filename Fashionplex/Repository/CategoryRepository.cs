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
    /// This class contains all the methods to complete CRUD (Create, read, update, delete) operation for the product category.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _dbContext;

        /// <summary>
        /// Initialize DbContext
        /// </summary>
        /// <param name="dbContext"></param>
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Category GetCategoryById(long id)
        {
            var category = _dbContext.Categories.Find(id);
            return category;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = _dbContext.Categories;
            return categories;
        }

        public void CreateCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }
    }
}
