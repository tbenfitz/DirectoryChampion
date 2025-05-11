using System.Web.Optimization;

namespace DirectoryChampionUI
{
   public class BundleConfig
   {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {         
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery/jquery-{version}.js")
                .Include("~/Scripts/jqueryUI/jquery-ui-{version}.js"));            

            bundles.Add(new ScriptBundle("~/bundles/popper")
                .Include("~/Scripts/umd/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout")
                .Include("~/Scripts/knockout-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/utilities")
               .Include("~/Scripts/utilities/utilities.js"));

            bundles.Add(new ScriptBundle("~/bundles/directory-champ")
                .Include("~/Scripts/ViewModels/DirectoryChampViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/jasmine")
                .Include("~/Scripts/jasmine/jasmine.js"));

            bundles.Add(new StyleBundle("~/Content/css")                
                .IncludeDirectory("~/Content/bootstrap", "*.css")
                .Include("~/Content/style.css"));
        }
   }
}