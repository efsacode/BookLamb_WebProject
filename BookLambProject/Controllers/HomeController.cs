using BookLambProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookLambProject.Data;
using BookLambProject.Utilities;

namespace BookLambProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookLambProjectContext _context;

        public HomeController(BookLambProjectContext context)
        {
            _context = context;
        }

        // Get Action
        public IActionResult Login()
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //Post Action
        [HttpPost]
        public ActionResult Login(User u)
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            if (HttpContext.Session.GetString("UserName") == null)
            {
                if (ModelState.IsValid)
                {       
                    
                 
                    var obj = _context.User.Where(a => a.UserName.Equals(u.UserName) && a.UserPassword.Equals(u.UserPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        HttpContext.Session.SetString("UserName", obj.UserName.ToString());
                        return RedirectToAction("Index", "Books");
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Logout()
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index","Books");
        }
       
        [Authentication]
        public IActionResult Index()
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.IsUserAuthenticated = HttpContext.Session.GetString("UserName") != null;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}