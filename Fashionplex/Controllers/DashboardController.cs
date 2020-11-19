using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fashionplex.Controllers
{
    /// <summary>
    /// This class controlls the admin dashboard page and prevent to access unauthorized users
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Display admin dashboard page
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}