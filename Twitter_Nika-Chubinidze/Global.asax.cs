using Domain.Concrete;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Twitter_Nika_Chubinidze.Infrastructure;
using Domain.Migrations; 

namespace Twitter_Nika_Chubinidze
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles); 
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwitterContext, Configuration>());
            using (var db = new TwitterContext())
            {
                db.Database.Initialize(true);
            }
            Configuration config = new Configuration();
            config.FillWithData();
        }
    }
}
