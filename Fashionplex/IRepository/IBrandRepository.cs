using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.IRepository
{
    public interface IBrandRepository
    {
       
        void CreateBrand(Brand brand);
        Brand FindBrandById(Guid id);
        IEnumerable<Brand> GetAllBrands();
        void UpdateBrand(Brand brand);
        void DeleteBrand(Brand brand);
    }
}
