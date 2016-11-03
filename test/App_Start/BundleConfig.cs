using System.Web;
using System.Web.Optimization;

namespace test
{
    public class BundleConfig
    {
        // 如需 Bundling 的詳細資訊，請造訪 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/detailCss").Include(
                      "~/Content/noside.css"));
            bundles.Add(new ScriptBundle("~/bundles/ProductDetail").Include(
                   "~/jQuery/Admin/ProductDetail.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/SaleReport").Include(
                   "~/jQuery/Admin/SaleReport.js"
               ));

            bundles.Add(new StyleBundle("~/Content/Home/Index").Include(
                   "~/Content/Home/Index.css"
               ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-theme.css"));
            bundles.Add(new StyleBundle("~/Content/dataTables/css").Include(
                    "~/Content/DataTables-1.10.0/css/jquery.dataTables.css",
                    "~/Content/DataTables-1.10.0/css/dataTables.bootstrap.css",
                    "~/Content/DataTables-1.10.0/css/dataTables.custom.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/scripts/noty").Include(
                   "~/Scripts/noty/jquery.noty.js",
                   "~/Scripts/noty/layouts/center.js",
                   "~/Scripts/noty/themes/default.js",
                   "~/jQuery/PlugIn/jquery-noty.plus.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/public").Include(
                   "~/jQuery/MainPage/ExtendMethod.js",
                   "~/jQuery/MainPage/AutoFill.js",
                   "~/jQuery/MainPage/ButtonEvent.js",
                   "~/jQuery/MainPage/PageKeyPress.js",
                   "~/jQuery/MainPage/SearchIcon.js",
                   "~/jQuery/PlugIn/jquery-currency.plus.js",
                   "~/Scripts/jquery.serialize-object.js",
                   "~/Scripts/bootstrap-datepicker.js",
                   "~/Scripts/locales/bootstrap-datepicker.zh-TW.js",
                   "~/jQuery/MainPage/Datepicker.js",
                   "~/jQuery/MainPage/ModalDialog.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/scripts/datatables").Include(
                    "~/Scripts/DataTables-1.10.0/jquery.dataTables.js",
                    "~/Scripts/DataTables-1.10.0/dataTables.bootstrap.js",
                    "~/Scripts/DataTables-1.10.0/dataTables.pipeline.js",
                    "~/Scripts/DataTables-1.10.0/dataTables-boker.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/detailMain").Include(
                    "~/jQuery/MainPage/DetailMain.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/ThreeJs").Include(
                     "~/jQuery/ThreeJs/three.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/3DTest").Include(
                      "~/jQuery/ThreeJs/OBJMTLLoader.js",
                      "~/jQuery/ThreeJs/MTLLoader.js",
                      "~/jQuery/ThreeJs/Detector.js",
                      "~/jQuery/html2canvas.js"));

            bundles.Add(new StyleBundle("~/Content/3DTest").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/FurnitureDecorate/FurnitureDecorate.css"));

            bundles.Add(new ScriptBundle("~/bundles/FurnitureDecorate").Include(
                      "~/jQuery/FurnitureDecorate/FurnitureDecorate.js"));

            bundles.Add(new StyleBundle("~/CSS/Content").Include(
                      "~/Content/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.form*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/datepicker.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-datepicker.css"
                ));
        }
    }
}