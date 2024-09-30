using Blogsite.DTOs;
using Blogsite.EF;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Blogsite.Controllers
{
    public class loginController : Controller
    {
        // Instantiate the database context
        

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Index(loginDTO l)
            
        {
            MydbEntities db = new MydbEntities();
            if (ModelState.IsValid)
            {
                var user = (from u in db.users
                            where u.username.Equals(l.username) &&
                                  u.password.Equals(l.password)
                            select u).SingleOrDefault();

                if (user == null)
                {
                    TempData["Msg"] = "User not found / Username and Password mismatch";
                    return RedirectToAction("signup","signup");
                }

               
                Session["user"] = user;
                TempData["Msg"] = "Login Successful";

                
                return RedirectToAction("Dashboard", "post"); 
            }

          
            return View(l);
        }

       
    }
}
