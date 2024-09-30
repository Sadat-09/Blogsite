using Blogsite.Auth;
using Blogsite.DTOs;
using Blogsite.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogsite.Controllers
{
    [Logged]
    public class postController : Controller
    {
        // GET: post
        MydbEntities db = new MydbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult addblog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addblog(postDTO s)
        {
            if (ModelState.IsValid)
            {
                var x = ((user)Session["user"]).id;
                s.UserId = x;
                var st = Convert(s);
                db.posts.Add(st);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s);
        }

        [HttpGet]
        public ActionResult viewlog()
        {
            var data = db.posts.ToList();
            var converted = Convert(data, db);
            return View(converted);
        }

        /*[HttpPost]
        public ActionResult viewblog(postDTO s)
        {
            if (ModelState.IsValid)
            {
                var x = ((user)Session["user"]).id;
                s.UserId = x;
                var st = Convert(s);
                db.posts.Add(st);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s);
        }*/

        [HttpGet]
        public ActionResult viewcontent(int id)
        {
            var data = db.posts.Find(id);
            var converted = Convert2(data);
            return View(converted);
        }
       

        [HttpPost]
        
        public ActionResult AddComment(int PostId, string CommentText)
        {
            if (Session["user"] == null)
            {
                TempData["Msg"] = "You need to log in to comment.";
                return RedirectToAction("Index", "Login");
            }

            var user = (user)Session["user"];
            var comment = new commentDTO()
            {
                PostId = PostId,
                UserId = user.id,
                contents = CommentText,
               
            };

            var converted = Convert3(comment);
            db.comments.Add(converted);
            db.SaveChanges();

            TempData["Msg"] = "Comment added successfully.";
            return RedirectToAction("viewlog");
        }

        public static postDTO Convert(post s, string username = null)
        {
            return new postDTO()
            {
                id = s.id,
                title = s.title,
                contents = s.contents,
                UserId = s.UserId,
                Username = username // Ensure this matches the property name in postDTO
            };
        }

        public static post Convert(postDTO s)
        {
            return new post()
            {
                id = s.id,
                title = s.title,
                contents = s.contents,
                UserId = s.UserId
            };
        }


        public static List<postDTO> Convert(List<post> posts, MydbEntities db)
        {
            var list = new List<postDTO>();
            foreach (var post in posts)
            {
                var username = db.users.Where(user => user.id == post.UserId).Select(user => user.username).FirstOrDefault();
                var st = Convert(post, username);
                list.Add(st);
            }
            return list;
        }
        public static postDTO Convert2(post s)
        {
            return new postDTO()
            {
                id = s.id,
                title = s.title,
                contents = s.contents,
                UserId = s.UserId,
                comments = s.comments.Select(c => new commentDTO
                {
                    id = c.id,
                    contents = c.contents,
                    PostId = c.PostId,
                    UserId = c.UserId
                }).ToList()

            };
        }

        public static post Convert2(postDTO s)
        {
            return new post()
            {
                id = s.id,
                title = s.title,
                contents = s.contents,
                UserId = s.UserId
            };
        }


        public static List<postDTO> Convert2(List<post> posts)
        {
            var list = new List<postDTO>();
            foreach (var post in posts)
            {
                
                var st = Convert(post);
                list.Add(st);
            }
            return list;
        }

        public static commentDTO Convert3(comment s)
        {
            return new commentDTO()
            {
                id = s.id,
                PostId = s.PostId,
                contents = s.contents,
                UserId = s.UserId,

            };
        }

        public static comment Convert3(commentDTO s)
        {
            return new comment()
            {
                id = s.id,
                PostId = s.PostId,
                contents = s.contents,
                UserId = s.UserId
            };
        }


        public static List<commentDTO> Convert3(List<comment> comments)
        {
            var list = new List<commentDTO>();
            foreach (var l in comments)
            {

                var st = Convert3(l);
                list.Add(st);
            }
            return list;
        }
    }
}
