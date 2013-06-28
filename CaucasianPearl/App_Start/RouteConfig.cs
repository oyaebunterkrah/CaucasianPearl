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
            //routes.IgnoreRoute("content/{*pathInfo}");

            routes.MapRoute(
                name: "Events",
                url: "events",
                defaults: new
                    {
                        controller = Consts.Controllers.Event.Name,
                        action = Consts.Actions.Index,
                        shortname = UrlParameter.Optional
                    });

            routes.MapRoute(
                name: "News",
                url: "news",
                defaults: new
                    {
                        controller = Consts.Controllers.OneNews.Name,
                        action = Consts.Actions.Index,
                        shortname = UrlParameter.Optional
                    });

            routes.MapRoute(
                name: "Profiles",
                url: "profiles",
                defaults: new
                    {
                        controller = Consts.Controllers.Profile.Name,
                        action = Consts.Actions.Index,
                        shortname = UrlParameter.Optional
                    });

            routes.MapRoute(
                name: "Feedbacks",
                url: "feedbacks",
                defaults: new
                    {
                        controller = Consts.Controllers.Feedback.Name,
                        action = Consts.Actions.Index
                    });

            routes.MapRoute(
                name: "Requests",
                url: "requests",
                defaults: new
                    {
                        controller = Consts.Controllers.Request.Name,
                        action = Consts.Actions.Index
                    });

            routes.MapRoute(
                name: "Gallery",
                url: "gallery/{action}/{photoId}/{photosetId}",
                defaults: new
                    {
                        controller = Consts.Controllers.Gallery.Name,
                        action = Consts.Actions.Index,
                    });

            routes.MapRoute(
                name: "ContentBlocks",
                url: "contentblocks",
                defaults: new
                    {
                        controller = Consts.Controllers.ContentBlock.Name,
                        action = Consts.Actions.Index
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
                name: "ProfileByShortName",
                url: "profiles/{shortname}",
                defaults: new
                    {
                        controller = Consts.Controllers.Profile.Name,
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
                        id = UrlParameter.Optional
                    });
        }
    }
}