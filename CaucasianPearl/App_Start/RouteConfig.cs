using System.Web.Mvc;
using System.Web.Routing;
using CaucasianPearl.Core.Constants;

namespace CaucasianPearl.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("Content/{*pathInfo}");

            routes.MapRoute(
                name: "Events",
                url: "events",
                defaults: new
                {
                    controller = Consts.Controllers.Event.Name,
                    action = Consts.Actions.Index,
                    shortname = UrlParameter.Optional
                });

            routes.MapRoute(name: "News",
                url: "news",
                defaults: new
                {
                    controller = Consts.Controllers.OneNews.Name,
                    action = Consts.Actions.Index,
                    shortname = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Feedbacks",
                url: "feedbacks",
                defaults: new
                {
                    controller = Consts.Controllers.Feedback.Name,
                    action = Consts.Actions.Index,
                });

            routes.MapRoute(
                name: "Requests",
                url: "requests",
                defaults: new
                {
                    controller = Consts.Controllers.Request.Name,
                    action = Consts.Actions.Index,
                });

            routes.MapRoute(
                name: "EventByShortName",
                url: "events/{shortname}",
                defaults: new
                {
                    controller = Consts.Controllers.Event.Name,
                    action = Consts.Actions.GetByShortName,
                    shortname = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "OneNewsByShortName",
                url: "news/{shortname}",
                defaults: new
                {
                    controller = Consts.Controllers.OneNews.Name,
                    action = Consts.Actions.GetByShortName,
                    shortname = UrlParameter.Optional
                });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                    new
                    {
                        controller = Consts.Controllers.Home.Name,
                        action = Consts.Actions.Index,
                        id = UrlParameter.Optional,
                    });
        }
    }
}