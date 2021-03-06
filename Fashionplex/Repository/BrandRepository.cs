﻿using Fashionplex.Data;
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
    /// This class contains all the methods to complete CRUD (Create, read, update, delete) operation for the product brands.
    /// </summary>
    public class BrandRepository : IBrandRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Initialize DbContext
        /// </summary>
        /// <param name="context"></param>
        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public Brand FindBrandById(Guid id)
        {
            var brand = _context.Brands.Find(id);
            return brand;
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            var brands = _context.Brands;
            return brands;
        }

        public void UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();
        }

        public void DeleteBrand(Brand brand)
        {
            _context.Brands.Remove(brand);
            _context.SaveChanges();
        }
    }
}
