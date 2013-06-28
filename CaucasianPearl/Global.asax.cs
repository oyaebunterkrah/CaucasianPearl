using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CaucasianPearl.App_Start;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Services.LoggingService;

namespace CaucasianPearl
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private static readonly ILogService LogFacade = DependencyResolverHelper<ILogService>.GetService();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // initialize database connection
            Initializer.Initialize();
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            string lang;
            return LocalizedCacheAttribute.GetVaryByCustomString(context, custom, out lang)
                       ? lang
                       : base.GetVaryByCustomString(context, custom);
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception is ThreadAbortException)
                return;

            LogFacade.Error(exception);
            //Response.Redirect(Consts.Controllers.Error.Actions.Unexpected);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            CultureHelper.Initialize();
        }
    }
}