using System.Web;
using System.Web.Optimization;

namespace InsuranceWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/script/app").
                        IncludeDirectory("~/app/blocks", "*.js", true).
                        Include("~/app/app.module.js").
                        IncludeDirectory("~/app/core", "*.module.js", true).
                        IncludeDirectory( "~/app/common" , "*.module.js",true).
                        IncludeDirectory("~/app/login", "*.module.js", true).
                        IncludeDirectory("~/app/main", "*.module.js", true).
                        IncludeDirectory("~/app", "*.route.js", true).
                        Include("~/app/core/constant.js").
                        Include("~/app/common/service/login.js",
                                "~/app/common/service/policy.js",
                                "~/app/common/service/setting.js").
                        Include("~/app/common/factory/user.js",
                                "~/app/common/factory/common.js").
                        Include("~/app/common/directive/file-upload.js").
                        Include(
                                "~/app/app.run.js",
                                "~/app/app.config.js",
                                "~/app/app.interceptor.js",
                                "~/app/app.controller.js").
                        Include(
                                "~/app/login/login/login.controller.js").
                        Include(
                                "~/app/main/main.run.js",
                                "~/app/main/main.config.js",
                                "~/app/main/main.controller.js",
                                "~/app/main/home/home.controller.js",
                                "~/app/main/settings/settings.controller.js").
                        Include(
                                "~/app/main/policy/new/new.controller.js",
                                "~/app/main/policy/current/current.controller.js",
                                "~/app/main/policy/expired/expired.controller.js"));

            bundles.Add(new ScriptBundle("~/script/core").Include(
                      "~/lib/js/jquery.js",
                      "~/lib/js/bootstrap.js",
                      "~/lib/js/angular.js",
                      "~/lib/js/angular-animate.js",
                      "~/lib/js/angular-sanitize.js",
                      "~/lib/js/angular-ui-router.js",
                      "~/lib/js/ngStorage.js",
                      "~/lib/js/angular-filter.min.js",
                      "~/lib/js/ui-bootstrap-tpls.min.js",
                      "~/lib/js/angular-toastr.tpls.min.js",
                      "~/lib/js/loading-bar.min.js",
                      "~/lib/js/angular-messages.min.js"));

            bundles.Add(new StyleBundle("~/css/core").Include(
                      "~/lib/css/bootstrap.min.css",
                      "~/lib/css/font-awesome.min.css",
                       "~/lib/css/angular-toastr.min.css",
                       "~/lib/css/loading-bar.min.css"));

            bundles.Add(new StyleBundle("~/css/app").
                      IncludeDirectory("~/assets", "*.css", true));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
        BundleTable.EnableOptimizations = true;
#endif

        }
    }
}
