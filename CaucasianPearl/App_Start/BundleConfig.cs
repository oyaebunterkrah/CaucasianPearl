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

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/content/js/sys/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/content/js/sys/jquery.unobtrusive*",
                "~/content/js/sys/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/content/js/sys/modernizr-*"));

            bundles.Add(new StyleBundle("~/content/styles").Include("~/content/css/site.css"));

            bundles.Add(new StyleBundle("~/content/themes/base/css").Include(
                "~/content/themes/base/jquery.ui.core.css",
                "~/content/themes/base/jquery.ui.resizable.css",
                "~/content/themes/base/jquery.ui.selectable.css",
                "~/content/themes/base/jquery.ui.accordion.css",
                "~/content/themes/base/jquery.ui.autocomplete.css",
                "~/content/themes/base/jquery.ui.button.css",
                "~/content/themes/base/jquery.ui.dialog.css",
                "~/content/themes/base/jquery.ui.slider.css",
                "~/content/themes/base/jquery.ui.tabs.css",
                "~/content/themes/base/jquery.ui.datepicker.css",
                "~/content/themes/base/jquery.ui.progressbar.css",
                "~/content/themes/base/jquery.ui.theme.css"));
        }
    }
}