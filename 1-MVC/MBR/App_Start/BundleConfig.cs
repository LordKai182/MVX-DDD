using System.Web;
using System.Web.Optimization;

namespace MBR
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/jquery-3.0.0.js",
                      "~/Scripts/bootstrap-select.js",
                    "~/Scripts/sweetalert2.all.min.js",
                          "~/Scripts/mvc-grid.js",
                          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/mvc-grid.css",
                       "~/Content/bootstrap-select.css",
                       "~/Content/SpinerLoad.css",
                       "~/Content/sweetalert2.min.css",
                      "~/Content/site.css"));
        }
    }
}
