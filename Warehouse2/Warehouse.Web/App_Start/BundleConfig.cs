using System.Web.Optimization;

namespace Warehouse.Web
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/site/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/jquery.fancybox.css",
                      "~/Content/css/jquery.nouislider.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/spa/css-1").Include("~/Content/css-1/*.css"));

            bundles.Add(new StyleBundle("~/Content/spa/css-2")
                .Include(
                "~/Content/css-2/pages/*.css",
                "~/Content/css-2/custom/*.css",
                "~/Content/css-2/components.css",
                "~/Content/css-2/plugins.css",
                "~/Content/css-2/layout.css",
                "~/Content/css-2/themes/darkblue.css",
                "~/Content/css-2/xtra.css"
            ));

            bundles.Add(new StyleBundle("~/Content/spa/css-fb")
                .Include(
                "~/Content/css-2/pages/*.css",
                "~/Content/css-2/custom/*.css",
                "~/Content/css-2/components.css",
                "~/Content/css-2/plugins.css",
                "~/Content/css-2/layout.css",
                "~/Content/css-2/themes/blue.css",
                "~/Content/css-2/xtra.css"
            ));

            bundles.Add(new StyleBundle("~/Content/spa/unsecured")
                .Include(
                "~/Content/css-2/pages/login.css",
                "~/Content/css-2/pages/error.css",
                "~/Content/css-2/components.css",
                "~/Content/css-2/plugins.css",
                "~/Content/css-2/layout.css",
                "~/Content/css-2/themes/darkblue.css",
                "~/Content/css-2/xtra.css"
            ));


            bundles.Add(new ScriptBundle("~/script/site")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/site/modernizr.custom.79639.js")
                .Include("~/Scripts/site/bootstrap.min.js")
                .Include("~/Scripts/site/retina.min.js")
                .Include("~/Scripts/site/scrollReveal.min.js")
                .Include("~/Scripts/site/jquery.ba-cond.min.js")
                .Include("~/Scripts/site/jquery.slitslider.js")
                .Include("~/Scripts/site/parallax.js")
                .Include("~/Scripts/site/jquery.counterup.min.js")
                .Include("~/Scripts/site/waypoints.min.js")
                .Include("~/Scripts/site/settings.js")
            );

            bundles.Add(new ScriptBundle("~/script/ie9")
                .Include("~/scripts/spa-1/respond.min.js")
                .Include("~/scripts/spa-1/excanvas.min.js")
             );

            bundles.Add(new ScriptBundle("~/script/vendors")
                .Include("~/scripts/jquery-{version}.js")
                .Include("~/scripts/jquery.migrate.min.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/angular.js")
                .Include("~/scripts/moment.js")
                .Include("~/scripts/spa-1/bootstrap-hover-dropdown.js")
                .Include("~/scripts/spa-1/jquery.slimscroll.js")
                .Include("~/scripts/spa-1/jquery.blockui.min.js")
                .Include("~/scripts/spa-1/jquery.cokie.min.js")
                .Include("~/scripts/spa-1/jquery.uniform.js")
                .Include("~/scripts/spa-1/bootstrap-switch.js")
                .Include("~/scripts/spa-1/ocLazyLoad.min.js")
                .Include("~/scripts/spa-1/ui-bootstrap-tpls.js")
                .Include("~/scripts/spa-1/metronic.js")
                .Include("~/scripts/spa-1/bootstrap-confirmation.js")
                .Include("~/scripts/spa-1/bootstrap-select.js")
                .Include("~/scripts/spa-1/select2.js")
                .Include("~/scripts/spa-1/jquery.multi-select.js")
                .Include("~/Scripts/spa-1/rzslider.js")
                .Include("~/Scripts/spa-1/toastr.min.js")
                .Include("~/Scripts/spa-1/metronic.js")
                .Include("~/Scripts/spa-1/layout.js")
                .Include("~/Scripts/vendors/*.js")
                .Include("~/Scripts/angular-*")
             );

            bundles.Add(new ScriptBundle("~/script/spa")
                .Include("~/app/app.js")
                .Include("~/scripts/modules/*.js")
                .Include("~/app/app.*")
                .Include("~/app/router/*.js")
                .Include("~/app/directives/*.js")
                .Include("~/app/services/*.js")
                .IncludeDirectory("~/app/views/", "*.js", true)
            );

        }
    }
}
