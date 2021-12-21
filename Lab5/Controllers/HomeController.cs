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
                var tag = new byte[AesGcm.TagByteSizes.MaxSize];
                password = _helper.Encrypt(password, _config, nonce,tag);
                if (BCrypt.Net.BCrypt.Verify(password, VerifyUser.Password))
                {
                    return Redirect("~/Home/Users");
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
                var tag = new byte[AesGcm.TagByteSizes.MaxSize];
                RandomNumberGenerator.Fill(nonce);
                password = _helper.Encrypt(password, _config, nonce,tag);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
                newUser.Mistery = Convert.ToBase64String(nonce);
                UserInfo newUserInfo = new UserInfo();
                newUserInfo.Name = user.Name;
                var nonceForCard = new byte[AesGcm.NonceByteSizes.MaxSize];
                RandomNumberGenerator.Fill(nonceForCard);
                newUserInfo.CreditCard = _helper.Encrypt(user.CreditCard, _config, nonceForCard, tag);
                tag = new byte[AesGcm.TagByteSizes.MaxSize];
                newUserInfo.PhoneNumber = _helper.Encrypt(user.PhoneNumber, _config, nonceForCard, tag);
                newUserInfo.Nonce = Convert.ToBase64String(nonceForCard);
                newUserInfo.Tag = Convert.ToBase64String(tag);
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
            IEnumerable<UserViewModel> users = db.Users.Join(db.UsersInfo, 
                U => U.Id, Ui => Ui.Id,(U,Ui)=>new UserViewModel() {
                    Name=Ui.Name, Email=U.Email, PhoneNumber= _helper.Decrypt(Ui.PhoneNumber,_config, Convert.FromBase64String(Ui.Nonce), Convert.FromBase64String(Ui.Tag))
                });
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
