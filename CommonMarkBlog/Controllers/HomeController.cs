using System.Web.Mvc;
using System.Web.UI.WebControls;
using CommonMarkBlog.Models;

namespace CommonMarkBlog.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var posts = DocumentSession.Query<Blog>()
                .OrderbyDesendence(b => b.CreatedDate)
                .ToArray();

            return View(posts);
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                blog = DocumentSession.Load<Blog>(id);
                return blog ? View() : View(blog);
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                DocumentSession.Store(blog);
                ViewBag.InfoMessage = "";
            }
            else
            {
                ViewBag.ErrorMessage = "";
            }

            return View(blog);
        }
    }
}