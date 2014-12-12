using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Embedded;

namespace CommonMarkBlog
{
    public class MvcApplication : HttpApplication
    {
        private static IDocumentStore _documentStore;
        public MvcApplication()
        {
            BeginRequest += (s, e) =>
            {
                HttpContext.Current.Items["CurrentRequestRavenSession"] = _documentStore.OpenSession();
            };

            EndRequest += (s, e) =>
            {
                using (var session = (IDocumentSession) HttpContext.Current.Items["CurrentRequestRavenSession"])
                {
                    if (session == null)
                        return;

                    if (Server.GetLastError() != null)
                        return;

                    session.SaveChanges();
                }
            };
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (_documentStore != null) return;

            _documentStore = new EmbeddableDocumentStore
            {
                DataDirectory = "App_Data",
                //UseEmbeddedHttpServer = true,
                //Configuration = {Port = 8888}
            }.Initialize();
        }
    }
}
