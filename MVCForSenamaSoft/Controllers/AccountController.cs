using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MVCForSenamaSoft.ViewModels;
using MVCForSenamaSoft.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using MVCForSenamaSoft.Services;
using Microsoft.Extensions.Logging;

namespace MVCForSenamaSoft.Controllers
{
    public class AccountController : Controller
    {
        private const string AdminUserName = "administrator";

        private readonly UsersContext _db;
        private readonly EmailService _service;

        public AccountController(UsersContext context, ILogger<AccountController> logger, EmailService service)
        {
            _db = context;
            _service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (Helpers.IsEmailValid(model.Email))
            {
                User user = _db.Users.FirstOrDefault(u => u.Email == model.Email); 
                if (user == null)
                {
                    _db.Users.Add(new User
                    {
                        Email = model.Email,
                        UserName = AdminUserName,
                        Domain = model.Email.Remove(model.Email.IndexOf("@")),
                        Password = Helpers.GenerateRandomPassword()
                    });

                    _db.SaveChanges();

                    var userEmail = new User
                    {
                        UserName = AdminUserName,
                        Domain = model.Email.Remove(model.Email.IndexOf("@")),
                        Password = Helpers.GenerateRandomPassword()
                    };

                    SendEmail(userEmail.Domain, userEmail.UserName, userEmail.Password, model.Email);

                    Authenticate(model.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "This mail is already busy");
                }
            }

            return View(model);
        }

        public void SendEmail(string domain, string userName, string password, string email)
        {
            _service.SendEmail(domain, userName, password, email);
        }

        private void Authenticate(string Email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, Email)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) 
            {
                return Content(String.Format("Hello {0}!!!", User.Identity.Name));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _db.Users.FirstOrDefault(
                    u => u.Domain == model.Domain &&
                    u.UserName == model.UserName &&
                    u.Password == u.Password);
                if (user != null)
                {
                    Authenticate(model.Domain);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect data");
            }

            return View(model);
        }
    }
}
