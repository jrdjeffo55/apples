using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apple.Models;
using Microsoft.EntityFrameworkCore;

namespace apple.Controllers
{
    public class IdeaController : Controller
    {
        private appleContext _context;

        public IdeaController(appleContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("allideas")]
        public IActionResult AllIdeas()
        {
            int? ThisUser = HttpContext.Session.GetInt32("UserId");
            if(ThisUser == null)
            {
                TempData["Login"] = "Must be logged in to view this page";
                return RedirectToAction("Index", "User");
            }
            User CurrentUser = _context.Users.SingleOrDefault(User => User.Id == (int)ThisUser);
            List<Idea> AllIdeas = _context.Ideas.OrderByDescending(Idea => Idea.Likes.Count).Include(Idea => Idea.Likes).Include(Idea => Idea.User).ToList();
            ViewBag.AllIdeas = AllIdeas;
            ViewBag.User = CurrentUser;
            ViewBag.Error = TempData["Error"];
            return View();
        }

        [HttpPost]
        [Route("allideas/addidea")]
        public IActionResult AddIdea(IdeaViewModel newIdea)
        {
            int? ThisUser = HttpContext.Session.GetInt32("UserId");
            if(ModelState.IsValid)
            {
                Idea NewIdea = new Idea
                {
                    UserId = (int)ThisUser,
                    Content = newIdea.Content
                };
                _context.Ideas.Add(NewIdea);
                _context.SaveChanges();
                return RedirectToAction("AllIdeas");
            }
            TempData["Error"] = "Ideas cannot be blank";
            return RedirectToAction("AllIdeas", newIdea);
        }

        [HttpGet]
        [Route("allideas/delete/{ideaId}")]
        public IActionResult Delete(int ideaId)
        {
            Idea DeleteIdea = _context.Ideas.SingleOrDefault(Idea => Idea.Id == ideaId);
            _context.Ideas.Remove(DeleteIdea);
            _context.SaveChanges();
            return RedirectToAction("AllIdeas");
        }

        [HttpGet]
        [Route("allideas/like/{ideaId}")]
        public IActionResult Like(int ideaId)
        {
            int? ThisUser = HttpContext.Session.GetInt32("UserId");
            Like NewLike = new Like
            {
                UserId = (int)ThisUser,
                IdeaId = ideaId,
            };
            _context.Likes.Add(NewLike);
            _context.SaveChanges();
            return RedirectToAction("AllIdeas");
        }


        [HttpGet]
        [Route("userdetails/{userId}")]
        public IActionResult UserDetails(int userId)
        {
            int? ThisUser = HttpContext.Session.GetInt32("UserId");
            if(ThisUser == null)
            {
                TempData["Login"] = "Must be logged in to view this page";
                return RedirectToAction("Index", "User");
            }
            User CurrentUser = _context.Users.Include(User => User.Ideas).Include(User => User.Likes).ThenInclude(Like => Like.User).SingleOrDefault(User => User.Id == userId);
            ViewBag.User = CurrentUser;
            return View();
        }

        [HttpGet]
        [Route("ideadetails/{ideaId}")]
        public IActionResult IdeaDetails(int ideaId)
        {
            int? ThisUser = HttpContext.Session.GetInt32("UserId");
            if(ThisUser == null)
            {
                TempData["Login"] = "Must be logged in to view this page";
                return RedirectToAction("Index", "User");
            }
            Idea CurrentIdea = _context.Ideas.Include(Idea => Idea.User).Include(Idea => Idea.Likes).ThenInclude(Like => Like.User).SingleOrDefault(Idea => Idea.Id == ideaId);
            ViewBag.Idea = CurrentIdea;
            return View();
        }

        [HttpGet]
        [Route("ideadetails/grabuser/{userId}")]
        public IActionResult GrabUser(int userId)
        {
            int ThisUser = userId;
            return RedirectToAction("UserDetails", (int)ThisUser);
        }

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}