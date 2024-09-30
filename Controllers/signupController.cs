using Blogsite.DTOs;
using Blogsite.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogsite.Controllers
{
    public class signupController : Controller
    {
        // GET: signup
        MydbEntities db = new MydbEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signup(userDTO s)
        {

            
            if (ModelState.IsValid)
            {
                var st = Convert(s);
                db.users.Add(st);
                db.SaveChanges();
                return RedirectToAction("Index", "login");
            }
            return View(s);

        }

        public static userDTO Convert(user s)
        {

            return new userDTO()
            {
                id = s.id,
                username = s.username,
                password = s.password,
                email = s.email
            };
        }
        public static user Convert(userDTO s)
        {
            return new user()
            {
                id = s.id,
                username = s.username,
                password = s.password,
                email = s.email
            };
        }
        public static List<userDTO> Convert(List<user> u)
        {
            var list = new List<userDTO>();
            foreach (var s in u)
            {
                var st = Convert(s);
                list.Add(st);
            }
            return list;
        }
    }
}