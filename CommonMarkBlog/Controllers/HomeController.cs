using System.Web.Mvc;
using CommonMarkBlog.Models;

namespace CommonMarkBlog.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(Blog blog)
        {
            if (ModelState.IsValid)
                DocumentSession.Store(blog);

            return View();
        }
    }
}