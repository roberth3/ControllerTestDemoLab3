using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;




namespace ControllerTestDemo
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        //public static Container Container { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AuthConfig.RegisterAuth();

            //// 1. Create a new Simple Injector container
            //var container = new Container();

            //// 2. Configure the container (register)
            //container.Register<IUserRepository>();

            ////container.RegisterSingle<ILogger>(() => new CompositeLogger(
            ////    container.GetInstance<DatabaseLogger>(),
            ////    container.GetInstance<MailLogger>()
            ////));

            //// 3. Optionally verify the container's configuration.
            //container.Verify();

            //// 4. Store the container for use by the MVC application.
            //MvcApplication.Container = container;
                        
        }
    }
}