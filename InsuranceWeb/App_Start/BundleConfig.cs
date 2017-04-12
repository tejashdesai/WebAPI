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
                                "~/app/common/service/policy.js").
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
                                "~/app/main/home/home.controller.js").
                        Include(
                                "~/app/main/policy/new/new.controller.js",
                                "~/app/main/policy/current/current.controller.js",
                                "~/app/main/policy/expired/expired.controller.js"));

            bundles.Add(new ScriptBundle("~/script/core").Include(
                      "~/bower_components/jquery/dist/jquery.js",
                      "~/bower_components/bootstrap/dist/js/bootstrap.js",
                      "~/bower_components/angular/angular.js",
                      "~/bower_components/angular-animate/angular-animate.js",
                      "~/bower_components/angular-sanitize/angular-sanitize.js",
                      "~/bower_components/angular-ui-router/release/angular-ui-router.js",
                      "~/bower_components/ngstorage/ngStorage.js",
                      "~/bower_components/angular-filter/dist/angular-filter.min.js",
                      "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",
                      "~/bower_components/angular-toastr/dist/angular-toastr.tpls.min.js",
                      "~/bower_components/angular-loading-bar/build/loading-bar.min.js",
                      "~/bower_components/angular-messages/angular-messages.min.js"));

            bundles.Add(new StyleBundle("~/css/core").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/bower_components/components-font-awesome/css/font-awesome.min.css",
                       "~/bower_components/angular-toastr/dist/angular-toastr.min.css",
                       "~/bower_components/angular-loading-bar/build/loading-bar.min.css"));

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
