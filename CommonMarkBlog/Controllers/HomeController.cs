using System;
using System.Linq;
using System.Web.Mvc;
using CommonMarkBlog.Models;
using Raven.Client.Linq;

namespace CommonMarkBlog.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var posts = DocumentSession.Query<Post>()
                .OrderByDescending(b => b.CreatedDate)
                .ToArray();

            return View(posts);
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var blog = DocumentSession.Load<Post>(id);
                if (blog == null)
                {
                    ViewBag.ErrorMessage = "The post does not exist.";
                    return View(new Post());
                }
                return View(blog);
            }

            return View(new Post());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                if (post.Id == 0)
                    post.CreatedDate = DateTime.Now;

                DocumentSession.Store(post);
                ViewBag.InfoMessage = "Post saved successfully.";
            }
            else
            {
                ViewBag.ErrorMessage = "The input is invalid.";
            }

            return View(post);
        }

        public ActionResult View(int? id)
        {
            if (id.HasValue)
            {
                var blog = DocumentSession.Load<Post>(id);

                if (blog == null)
                {
                    ViewBag.ErrorMessage = "The post does not exist.";
                    return View();
                }

                return View(blog);
            }

            return View();
        }
    }
}