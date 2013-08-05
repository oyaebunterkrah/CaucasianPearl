using System.Web.Optimization;
using CaucasianPearl.Core.Constants;

namespace CaucasianPearl.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/content/js/sys/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/content/js/sys/jquery.unobtrusive*",
                "~/content/js/sys/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/content/js/sys/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/content/css/site/site.css"));

            // jqueryui {
            bundles.Add(new StyleBundle(Format(Consts.Paths.PluginsPrefix, "jqueryui/css")).Include(
                Format(Consts.Paths.PluginsPrefix, "jqueryui/css/*.css")));
            
            bundles.Add(new ScriptBundle(Format(Consts.Paths.PluginsPrefix, "jqueryui/js")).Include(
                Format(Consts.Paths.PluginsPrefix, "jqueryui/js/*.js"))); // }

            // orbit {
            bundles.Add(new StyleBundle(Format(Consts.Paths.PluginsPrefix, "orbit/css")).Include(
                Format(Consts.Paths.PluginsPrefix, "orbit/*.css")));

            bundles.Add(new ScriptBundle(Format(Consts.Paths.PluginsPrefix, "orbit/js")).Include(
                Format(Consts.Paths.PluginsPrefix, "orbit/*.js"))); // }
        }

        private static string Format(string prefix, string path)
        {
            return string.Format("{0}/{1}", prefix, path);
        }
    }
}