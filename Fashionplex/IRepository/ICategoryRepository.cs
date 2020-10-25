using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.IRepository
{
   public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        Category GetCategoryById(long id);
        IEnumerable<Category> GetAllCategories();
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
