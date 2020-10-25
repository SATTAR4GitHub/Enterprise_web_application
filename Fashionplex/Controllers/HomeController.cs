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
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult AddImage()
        {
            return View();
        }

        //POST: Adding image for banner and logo
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
