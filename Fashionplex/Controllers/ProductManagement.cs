using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fashionplex.Data;
using Fashionplex.Models;
using Microsoft.AspNetCore.Authorization;
using Fashionplex.Enums;

namespace Fashionplex.Controllers
{
    /// <summary>
    /// This class handles product management: create new product, view and update product information, delete products. 
    /// Only adminhas access to this page to control over the products.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class ProductManagement : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor to initialize DbContext 
        /// </summary>
        /// <param name="context"></param>
        public ProductManagement(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: ProductManagement
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="searchString"></param>
        /// <param name="currentFilter"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string sortOrder, string searchString,
            string currentFilter, int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProductNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "productName_asc" : "";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Filter parameter
            ViewData["CurrentFilter"] = searchString;

            var products = from p in _context.Products
                           select p;
                          

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString));
                //products = _context.Products.Include(p => p.Brand).Include(p => p.Category);
            }

            switch (sortOrder)
            {
                case "productName_asc":
                    products = products.OrderBy(p => p.ProductName);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(p => p.CreateDate);
                    break;
                default:
                    products = products.OrderBy(p => p.Id);
                    break;
            }

            int pageSize = 10;

            return View(await Pagination<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        /// <summary>
        /// GET: ProductManagement/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// GET: ProductManagement/Create
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");

            // Method for product size and status enum drop down 
            ProductSizeDropDown();
            ProductStatusDropDown();

            return View();
        }

        /// <summary>
        /// POST: ProductManagement/Create
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,Size,Description,Slug,MetaDescription,MetaKeywords,SKU,Price,SalePrice,OldPrice,ImageUrl,QuantityInStock,IsBestseller,IsFeatured,CategoryId,BrandId,ProductStatus,Id,CreateDate,ModifiedDate,IsDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        /// <summary>
        /// GET: ProductManagement/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);

            // Method for product size and product status enum drop down 
            ProductSizeDropDown();
            ProductStatusDropDown();

            return View(product);
        }

      
        /// <summary>
        /// POST: ProductManagement/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(long id, [Bind("ProductName,Size,Description,Slug,MetaDescription,MetaKeywords,SKU,Price,SalePrice,OldPrice,ImageUrl,QuantityInStock,IsBestseller,IsFeatured,CategoryId,BrandId,ProductStatus,Id,CreateDate,ModifiedDate,IsDeleted")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        /// <summary>
        /// GET: ProductManagement/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// POST: ProductManagement/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Helper method to check if any product exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        /// <summary>
        /// Helper method for product status enum drop down
        /// </summary>
        private void ProductSizeDropDown()
        {
            var productSize = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select",
                    Value = " "
                }
            };

            foreach (Size item in Enum.GetValues(typeof(Size)))
            {
                productSize.Add(new SelectListItem { Text = Enum.GetName(typeof(Size), item), Value = item.ToString() });
            }

            ViewBag.ProductSize = productSize;
        }

        /// <summary>
        /// Helper method for product status enum drop down
        /// </summary>
        private void ProductStatusDropDown()
        {
            var productStatus = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select",
                    Value = " "
                }
            };

            foreach (ProductStatus item in Enum.GetValues(typeof(ProductStatus)))
            {
                productStatus.Add(new SelectListItem { Text = Enum.GetName(typeof(ProductStatus), item), Value = item.ToString() });
            }

            ViewBag.ProductStatus = productStatus;
        }
    }
}
