using Lab5.Data;
using Lab5.Data.Entity;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDB db;

        public HomeController(ILogger<HomeController> logger, ApplicationDB context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel user)
        {
            if(ModelState.IsValid)
            {
                string password = Lab4.Make_md5.GetHash(user.Password);
                var VerifyUser = db.Users.Where(u => u.Email == user.Email).FirstOrDefault();
                if (BCrypt.Net.BCrypt.Verify(password,VerifyUser.Password))
                {
                    return Redirect("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                }
            }
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegisterViewModel user)
        {
            if(ModelState.IsValid)
            {
                User newUser = new User();
                newUser.Email = user.Email;
                string password = Lab4.Make_md5.GetHash(user.Password);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
                db.Users.Add(newUser);                
                db.SaveChanges();
                return Redirect("~/Home/Index");
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
