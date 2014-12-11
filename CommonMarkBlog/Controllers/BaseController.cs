using System.Web.Mvc;
using Raven.Client;

namespace CommonMarkBlog.Controllers
{
    public abstract class BaseController : Controller
    {
        public IDocumentSession DocumentSession { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            DocumentSession = (IDocumentSession) HttpContext.Items["CurrentRequestRavenSession"];
        }
    }
}