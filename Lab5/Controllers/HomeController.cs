using Lab5.Data;
using Lab5.Data.Entity;
using Lab5.Helpers;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDB db;
        private IHelper _helper;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, ApplicationDB context, IHelper helper, IConfiguration config)
        {
            _logger = logger;
            db = context;
            _helper = helper;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                string password = Lab4.Make_md5.GetHash(user.Password);
                var VerifyUser = db.Users.Where(u => u.Email == user.Email).FirstOrDefault();
                byte[] nonce = Convert.FromBase64String(VerifyUser.Mistery);
                password = _helper.Encrypt(password, _config, nonce);
                if (BCrypt.Net.BCrypt.Verify(password, VerifyUser.Password))
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
            if (ModelState.IsValid)
            {
                User newUser = new User();
                newUser.Email = user.Email;
                string password = Lab4.Make_md5.GetHash(user.Password);
                var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
                RandomNumberGenerator.Fill(nonce);
                password = _helper.Encrypt(password, _config, nonce);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
                newUser.Mistery = Convert.ToBase64String(nonce);
                UserInfo newUserInfo = new UserInfo();
                newUserInfo.Name = user.Name;
                newUserInfo.PhoneNumber = user.PhoneNumber;
                newUserInfo.CreditCard = user.CreditCard;
                newUser.UserInfo = newUserInfo;
                newUserInfo.User = newUser;
                db.Users.Add(newUser);
                db.UsersInfo.Add(newUserInfo);
                db.SaveChanges();
                return Redirect("~/Home/Index");
            }
            return View();
        }

        public IActionResult Users()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            return View(users);
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
