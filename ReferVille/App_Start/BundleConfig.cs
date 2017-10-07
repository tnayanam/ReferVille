using System.Web.Optimization;

namespace ReferVille
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                       "~/Scripts/CustomJS.js",
                     "~/Scripts/jquery.superslides.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootbox.min.js"));

            //TODO: Adding BootSwatch Lumen theme caused on issus check developer tool
            //https://stackoverflow.com/questions/46508793/how-to-solve-glyphicons-halflings-regular-woff2-err-aborted-issue-in-asp-net-mvc
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      // "~/Content/bootstrap.css",
                      "~/Content/bootstrap-lumen.css",
                      "~/Content/superslides.css",
                      "~/Content/site.css"));
        }
    }
}
