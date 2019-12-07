using Login.Models;
using Login.Processors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly DatabaseManager _databaseManager;
        private readonly AppDb _db;

        public LoginController(ILogger<AdminController> logger, DatabaseManager databaseManager, AppDb db)
        {
            _logger = logger;
            _databaseManager = databaseManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = SHA1.Encode(user.Password);
                if (user.IsValid(user.UserName, user.Password, _db))
                {
                    //This needs to be refactored to work with .NET Core
                    //FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    return RedirectToAction("Index", "Client");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            //This needs to be refactored to work with .NET Core
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }

    public static class SHA1
    {
        public static string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}
