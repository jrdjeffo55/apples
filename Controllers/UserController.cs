using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apple.Models;

namespace apple.Controllers
{
    public class UserController : Controller
    {
        private appleContext _context;

        public UserController(appleContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Temp = TempData["Login"];
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel newUser)
        {
            if(ModelState.IsValid)
            {
                User DbUser = _context.Users.SingleOrDefault(User => User.Email == newUser.Email);
                if(DbUser != null)
                {
                    ViewBag.Exists = "Invalid Username";
                    return View("Index");
                }
                User NewUser = new User
                {
                    Name = newUser.Name,
                    Alias = newUser.Alias,
                    Email = newUser.Email,
                    Password = newUser.Password
                };
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                User CurrentUser = _context.Users.SingleOrDefault(User => User.Email == NewUser.Email);
                HttpContext.Session.SetInt32("UserId", CurrentUser.Id);
                return RedirectToAction("AllIdeas", "Idea");
            }
            return View("Index", newUser);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            User DbUser = _context.Users.SingleOrDefault(User => User.Email == email);
            if(DbUser == null)
            {
                ViewBag.Error = "Invalid Credentials";
                return View("Index");
            }
            if((string)DbUser.Email == email && (string)DbUser.Password == password)
            {
                HttpContext.Session.SetInt32("UserId", (int)DbUser.Id);
                return RedirectToAction("AllIdeas", "Idea");
            }
            ViewBag.Error = "Invalid Credentials";
            return View("Index");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}