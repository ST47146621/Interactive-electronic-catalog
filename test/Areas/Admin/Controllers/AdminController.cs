using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Areas.Admin.Services;
using test.Extend;
using test.Models;
using test.Services;
using PagedList;

namespace test.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ProductIndex", "Admin");
        }
        #region 使用中
        # region 商品頁面 ProductIndex
        #region 商品列表 Index+Partial
        public ActionResult ProductIndex()
        {
            ViewBag.Head = "ARealShop商品管理";
            ViewBag.Ul = "商品管理";
            return View();
        }
        public ActionResult ProductIndexPartial(int page = 1)
        {
            ProductService service = new ProductService();
            int pageSize = 15;
            int currentpage = page < 1 ? 1 : page;
            var model = service.Get();
            return PartialView(model.ToPagedList(currentpage, pageSize));
        }
        #endregion
        #region 商品新增
        public ActionResult ProductCreate()
        {
            TableService service = new TableService();
            return PartialView(service.GetCreate());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate(ProductModel model)
        {
            ProductService service = new ProductService();
            service.Create(model);
            return RedirectToAction("ProductIndex");
        }
        public JsonResult CheckProductId(string ProductId)
        {
            ProductService service = new ProductService();
            if (service.CheckProductId(ProductId))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("商品編號已存在", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region 商品修改

        public ActionResult ProductEdit(string Id)
        {
            ProductService service = new ProductService();
            return PartialView(service.GetEditData(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(ProductNoValidModel model)
        {
            ProductService service = new ProductService();
            service.Edit(model);
            return RedirectToAction("ProductIndex");
        }
        #endregion
        #region 商品刪除
        public ActionResult ProductDelete(string Id)
        {
            ProductService service = new ProductService();
            service.Delete(Id);
            return RedirectToAction("ProductIndex");
        }
        #endregion
        #endregion
        #region 購物單頁面 ShopCartIndex
        public ActionResult ShopCartIndex()
        {
            ShopCartService service = new ShopCartService();
            ViewBag.Head = "ARealShop購物單管理";
            ViewBag.Ul = "購物單管理";
            return View(service.Get());
        }
        public ActionResult ShopCartDetail(string Id)
        {
            ShopCartService service = new ShopCartService();
            return PartialView(service.GetDetail(Id));
        }
        #endregion
        #region 銷售量比較Api
        public ActionResult WeekIndex()
        {
            ViewBag.Head = "周銷售量比較";
            ViewBag.Ul = "周銷售量比較";
            return View();
        }
        public ActionResult MonthIndex()
        {
            ViewBag.Head = "月銷售量比較";
            ViewBag.Ul = "月銷售量比較";
            return View();
        }
        public ActionResult YearIndex()
        {
            ViewBag.Head = "年銷售量比較";
            ViewBag.Ul = "年銷售量比較";
            return View();
        }
        #endregion
        #region ProudctDetail 商品資料查詢 ProductDetail

        public ActionResult ProductDetail(string target)
        {
            ProductDetailService services = new ProductDetailService();

            ViewBag.Path = target;
            ViewBag.SelectorListItem = new List<SelectListItem> { 
                new SelectListItem { Text = "商品編號", Value = "0" },
                new SelectListItem { Text = "商品名稱", Value = "1" }
            };
            ViewBag.SignListItem = services.GetSignListItem();
            ViewBag.ConditionListItem = services.GetConditionListItem();
            return View(services.GetViewData());
        }

        public ActionResult ProductDetail_JsonResult(ProductDetailModel JsonData)
        {
            ProductDetailService services = new ProductDetailService();
            return Json(services.Search(JsonData), JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region SaleReport 損益表 SaleReportDetail

        public ActionResult SaleReport(string target)
        {
            SaleReportService services = new SaleReportService();

            ViewBag.Path = target;
            ViewBag.SelectorListItem = new List<SelectListItem> { 
                new SelectListItem { Text = "損益表年度/月", Value = "0" }
            };
            ViewBag.SignListItem = services.GetSignListItem();
            ViewBag.ConditionListItem = services.GetConditionListItem();
            return View(services.GetViewData());
        }

        public ActionResult SaleReport_JsonResult(SaleReportModel JsonData)
        {
            SaleReportService services = new SaleReportService();
            return Json(services.Search(JsonData), JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region SaleReportApi 損益表Api

        public ActionResult SaleReportMonth(bool total, bool income, bool final)
        {
            SaleReportService services = new SaleReportService();
            var res = services.GetMonthReport(total, income, final);
            if (res == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleReportSeason(bool total, bool income, bool final)
        {
            SaleReportService services = new SaleReportService();
            var res = services.GetSeasonReport(total, income, final);
            if (res == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleReportYear(bool total, bool income, bool final)
        {
            SaleReportService services = new SaleReportService();
            var res = services.GetYearReport(total, income, final);
            if (res == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region 分析推廣度 頁面
        public ActionResult IsPush()
        {
            IsPushService services = new IsPushService();
            return View();
        }
        #endregion
        #region 分析推廣度Api
        public ActionResult IsPushApi()
        {
            IsPushService services = new IsPushService();
            var res = services.GetIsPush();
            if (res == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
        # region 種類頁面
        public ActionResult CategoryIndex()
        {
            CategoryService service = new CategoryService();
            ViewBag.Head = "ARealShop種類管理";
            ViewBag.Ul = "種類管理";
            return View(service.Get());
        }
        public ActionResult CategoryCreate()
        {
            TableService service = new TableService();
            CategoryModel model = new CategoryModel();
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate(CategoryModel model)
        {
            CategoryService service = new CategoryService();
            service.Create(model);
            return RedirectToAction("CategoryIndex");
        }
        public ActionResult CategoryEdit(int Id)
        {
            CategoryService service = new CategoryService();
            return PartialView(service.GetEditData(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit(CategoryNoValidModel model)
        {
            CategoryService service = new CategoryService();
            service.Edit(model);
            return RedirectToAction("CategoryIndex");
        }
        public ActionResult CategoryDelete(int Id)
        {
            CategoryService service = new CategoryService();
            service.Delete(Id);
            return RedirectToAction("CategoryIndex");
        }
        #endregion
        # region 顏色頁面
        public ActionResult ColorIndex()
        {
            ColorService service = new ColorService();
            ViewBag.Head = "ARealShop顏色管理";
            ViewBag.Ul = "顏色管理";
            return View(service.Get());
        }
        public ActionResult ColorCreate()
        {
            TableService service = new TableService();
            ColorModel model = new ColorModel();
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ColorCreate(ColorModel model)
        {
            ColorService service = new ColorService();
            service.Create(model);
            return RedirectToAction("ColorIndex");
        }
        public ActionResult ColorEdit(int Id)
        {
            ColorService service = new ColorService();
            return PartialView(service.GetEditData(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ColorEdit(ColorNoValidModel model)
        {
            ColorService service = new ColorService();
            service.Edit(model);
            return RedirectToAction("ColorIndex");
        }
        public ActionResult ColorDelete(int Id)
        {
            ColorService service = new ColorService();
            service.Delete(Id);
            return RedirectToAction("ColorIndex");
        }
        #endregion
        # region 消息頁面
        public ActionResult NewsIndex()
        {
            NewsService service = new NewsService();
            ViewBag.Head = "ARealShop最新消息管理";
            ViewBag.Ul = "最新消息管理";
            return View(service.Get());
        }
        public ActionResult NewsCreate()
        {
            TableService service = new TableService();
            NewsModel model = new NewsModel();
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewsCreate(NewsModel model)
        {
            NewsService service = new NewsService();
            service.Create(model);
            return RedirectToAction("NewsIndex");
        }
        public ActionResult NewsEdit(int Id)
        {
            NewsService service = new NewsService();
            return PartialView(service.GetEditData(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewsEdit(NewsNoValidModel model)
        {
            NewsService service = new NewsService();
            service.Edit(model);
            return RedirectToAction("NewsIndex");
        }
        public ActionResult NewsDelete(int Id)
        {
            NewsService service = new NewsService();
            service.Delete(Id);
            return RedirectToAction("NewsIndex");
        }
        #endregion
        #region 帳單管理
        public ActionResult History()
        {
            return View();
        }
        public ActionResult HistoryPartial(int page = 1)
        {
            ShopCartService service = new ShopCartService();
            int pageSize = 10;
            int currentpage = page < 1 ? 1 : page;
            var model = service.GetHistory();
            return PartialView(model.ToPagedList(currentpage, pageSize));
        }
        #endregion
        #endregion

        #region 廢棄物
        #region 會員頁面
        public ActionResult MemberIndex()
        {
            MemberService service = new MemberService();
            ViewBag.Head = "ARealShop會員管理";
            ViewBag.Ul = "會員管理";
            return View(service.Get());
        }
        public ActionResult MemberCreate()
        {
            MemberService service = new MemberService();
            MemberModel model = new MemberModel();
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberCreate(MemberModel model)
        {
            MemberService service = new MemberService();
            service.Create(model);
            return RedirectToAction("MemberIndex");
        }
        public ActionResult MemberEdit(string Id)
        {
            MemberService service = new MemberService();
            return PartialView(service.GetEditData(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberEdit(MemberEditModel model)
        {
            MemberService service = new MemberService();
            service.Edit(model);
            return RedirectToAction("MemberIndex");
        }
        public ActionResult MemberDelete(string Id)
        {
            MemberService service = new MemberService();
            service.Delete(Id);
            return RedirectToAction("MemberIndex");
        }
        #endregion
        #region 熱門商品推薦
        public ActionResult HotIndex()
        {
            ViewBag.Head = "熱門商品推薦";
            ViewBag.Ul = "熱門商品推薦";
            return View();
        }
        #endregion
        #region 銷售量比較Api
        //public ActionResult WeekChart()
        //{
        //    ChartsService serivces = new ChartsService();
        //    return Json(serivces.GetWeekTotal(), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult MonthChart()
        //{
        //    ChartsService serivces = new ChartsService();
        //    return Json(serivces.GetMonthTotal(), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult YearChart()
        //{
        //    ChartsService serivces = new ChartsService();
        //    return Json(serivces.GetYearTotal(), JsonRequestBehavior.AllowGet);
        //}
        public ActionResult WeekChart(string data)
        {
            string[] productlist = JsonConvert.DeserializeObject<string[]>(data);
            ChartsService services = new ChartsService();
            var res = services.GetWeekTotal(productlist);
            if (res == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MonthChart(string data)
        {
            string[] productlist = JsonConvert.DeserializeObject<string[]>(data);
            ChartsService services = new ChartsService();
            var res = services.GetMonthTotal(productlist);
            if (res == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YearChart(string data)
        {
            string[] productlist = JsonConvert.DeserializeObject<string[]>(data);
            ChartsService services = new ChartsService();
            var res = services.GetYearTotal(productlist);
            if (res == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 熱門商品推薦Api
        public ActionResult HotChart()
        {
            ChartsService serivces = new ChartsService();
            return Json(serivces.GetHotTotal(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 新ChartAPI
        public ActionResult TestChart(string data)
        {
            string[] productlist = JsonConvert.DeserializeObject<string[]>(data);
            ChartsService services = new ChartsService();
            var res = services.GetTestTotal(productlist);
            if (res == null)
            {
                return Json("null", JsonRequestBehavior.AllowGet);
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public void AddWeek(string y, string m, string d)
        {
            ChartsService services = new ChartsService();
            services.AddWeek(y, m, d);
        }
        public void AddMonth(string y, string m)
        {
            ChartsService services = new ChartsService();
            
            services.AddWeek(y, m, "1");
            services.AddWeek(y, m, "8");
            services.AddWeek(y, m, "15");
            services.AddWeek(y, m, "22");
            services.AddWeek(y, m, "28");

            //services.AddPayMent(y, m);
        }
        public void AddPayment(string y, string m)
        {
            ChartsService services = new ChartsService();
            services.AddPayMent(y, m);
        }
        #endregion
        #endregion
    }
}
