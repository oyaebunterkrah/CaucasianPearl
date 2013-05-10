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
using CaucasianPearl.Core.Services;
using CaucasianPearl.Core.Services.Logging;

namespace CaucasianPearl
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private static readonly ILogFacade LogFacade = DependencyResolverHelper<ILogFacade>.GetService();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

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
            LogFacade.Error("Application_Error");
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // очень важно проверять готовность объекта сессии.
            if (HttpContext.Current.Session != null)
            {
                var ci = (CultureInfo)Session[Consts.SessionKeys.Culture];

                if (ci == null)
                {
                    var culture = Consts.Culture.Ru;

                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                        culture = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);

                    ci = new CultureInfo(culture);
                    Session[Consts.SessionKeys.Culture] = ci;
                }

                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }
    }
}