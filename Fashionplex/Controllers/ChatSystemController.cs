using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fashionplex.Data;
using Fashionplex.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fashionplex.Controllers
{
    /// <summary>
    /// This class handle the real time chat system included in the application.
    /// </summary>
    [Authorize]
    public class ChatSystemController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Intilize DbContext and user manager to handle users
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        public ChatSystemController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Display name and messages in the chat window
        /// </summary>
        /// <returns></returns>        
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.FirstName + " " + currentUser.LastName;
            }
            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }

        /// <summary>
        /// Saves messages to the message table
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserFullName = sender.FirstName + " " + sender.LastName;
                message.UserID = sender.Id;
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return Error();
        }

        /// <summary>
        /// Error method to handle error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}