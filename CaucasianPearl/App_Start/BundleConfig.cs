using System.Web.Optimization;
using CaucasianPearl.Core.Constants;

namespace CaucasianPearl.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                Format(Consts.Paths.Css.SiteCssPrefixPath, "site.css"), new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                Format(Consts.Paths.Js.SysJsPrefixPath, "jquery-{version}.js")));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                Format(Consts.Paths.Js.SysJsPrefixPath, "jquery.validate*"),
                Format(Consts.Paths.Js.SysJsPrefixPath, "jquery.unobtrusive*"),
                Format(Consts.Paths.Js.SysJsPrefixPath, "jquery.validate.unobtrusive*")));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                Format(Consts.Paths.Js.SysJsPrefixPath, "modernizr-*")));
        }

        private static string Format(string prefix, string path)
        {
            return string.Format("{0}/{1}", prefix, path);
        }
    }
}