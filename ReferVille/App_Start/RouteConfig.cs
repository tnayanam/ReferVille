using System.Web.Mvc;
using System.Web.Routing;

namespace ReferVille
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Referrer",
               url: "Referrer/{action}/{id}",
               defaults: new { controller = "Referrer", action = "ReferrerCenter", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Referral",
               url: "Referral/{action}/{id}",
               defaults: new { controller = "Referral", action = "ReferralCenter", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Resume",
               url: "Resume/{action}/{id}",
               defaults: new { controller = "Resume", action = "ResumeCenter", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "CoverLetter",
               url: "CoverLetter/{action}/{id}",
               defaults: new { controller = "CoverLetter", action = "CoverLetterCenter", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
