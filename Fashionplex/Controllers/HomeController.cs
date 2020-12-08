using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fashionplex.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Fashionplex.Data;
using Fashionplex.ViewModels;

namespace Fashionplex.Controllers
{
    /// <summary>
    /// This class handles Home page of the application.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        
        /// <summary>
        /// Initialize the web hosting environment where the application is running in
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Return the Home page view of the application
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: AddImage 
        /// </summary>
        /// <returns></returns>
        public IActionResult AddImage()
        {
            return View();
        }

        /// <summary>
        /// POST: Adding image for banner and logo
        /// </summary>
        /// <param name="imageModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddImage(ImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName;

                if (imageModel.Photos != null && imageModel.Photos.Count > 0)
                {
                    foreach (IFormFile phot in imageModel.Photos)
                    {
                        string folder = "images/banner-logo/";
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, folder);
                        uniqueFileName = phot.FileName;

                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        await phot.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    }
                }
                return RedirectToAction("index");
            }
            return View();
        }

        /// <summary>
        /// Method to display privacy statement of the company
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Method to display promotional offer of the company. 
        /// </summary>
        /// <returns></returns>
        public IActionResult Promotion()
        {
            return View();
        }

        /// <summary>
        /// Method for error handing
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
