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
            routes.IgnoreRoute("content/{*pathInfo}");

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
                name: "Sponsors",
                url: "sponsors",
                defaults: new
                    {
                        controller = Consts.Controllers.Sponsor.Name,
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
                name: "Members",
                url: "members",
                defaults: new
                    {
                        controller = Consts.Controllers.Profile.Name,
                        action = Consts.Controllers.Profile.Actions.Members,
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
                name: "Affiche",
                url: "affiche",
                defaults: new
                    {
                        controller = Consts.Controllers.Home.Name,
                        action = Consts.Controllers.Home.Actions.Affiche
                    });

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new
                    {
                        controller = Consts.Controllers.Home.Name,
                        action = Consts.Controllers.Home.Actions.About
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
                name: "SiteSettings",
                url: "sitesettings",
                defaults: new
                    {
                        controller = Consts.Controllers.SiteSettings.Name,
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
                name: "SponsorByShortName",
                url: "sponsors/{shortname}",
                defaults: new
                    {
                        controller = Consts.Controllers.Sponsor.Name,
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
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                    {
                        controller = Consts.Controllers.Home.Name,
                        action = Consts.Actions.Index,
                        id = UrlParameter.Optional
                    });
        }
    }
}