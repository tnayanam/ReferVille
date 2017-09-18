using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReferVille.Helpers
{
    public static class ActiveMenuHelper
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string icons)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            var currentArea = htmlHelper.ViewContext.RouteData.DataTokens["area"];
            // Build the icon and display text elements
            StringBuilder innerHtml = new StringBuilder();
            TagBuilder icon = new TagBuilder("i");
            icon.AddCssClass(icons);
            innerHtml.Append(icon.ToString());
            TagBuilder span = new TagBuilder("span");
            span.AddCssClass("css-leftpad");
            span.InnerHtml = linkText;
            innerHtml.Append(span.ToString());
            // Build the link
            TagBuilder link = new TagBuilder("a");
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string url = urlHelper.Action(actionName, controllerName, routeValues);
            link.MergeAttribute("href", url);
            link.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            link.InnerHtml = innerHtml.ToString();
            if (String.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase))
            {
                link.AddCssClass("active");
            }
            // Return the html
            return new MvcHtmlString(link.ToString());
        }
    }

}


