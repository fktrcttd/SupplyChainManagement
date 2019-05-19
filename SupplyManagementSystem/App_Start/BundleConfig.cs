using System.Web.Optimization;

namespace SCM
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js", "~/Scripts/chosen.jquery.js"));
            
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                // uncomment below if using the Scheduler
                // "~/Scripts/kendo/kendo.timezones.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js",
                "~/Scripts/Kendo/cultures/kendo.culture.ru-RU.min.js",
                "~/Scripts/Kendo/messages/kendo.messages.ru-RU.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                /*"~/Content/kendo/kendo.common-bootstrap.min.css",*/
                "~/Content/kendo/kendo.bootstrap-v4.min.css"));
            

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/site.css",
                "~/Content/simple-sidebar.css",
                "~/Content/bootstrap-vertical-tabs.css",
                "~/Content/chosen.css"
                ));
        }
    }
}
