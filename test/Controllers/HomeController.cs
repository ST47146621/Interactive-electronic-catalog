using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Extend;
using test.Models;
using test.Services;
using PagedList;

namespace test.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private int pageSize = 4;
        //
        // GET: /Home/
        #region 會員登入
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            LoginModel loginModel = new LoginModel();

            if (Request.Cookies["UIC"] != null)
            {
                if (!String.IsNullOrEmpty(Request.Cookies["UIC"]["RememberMe"]))
                {
                    loginModel.account = Request.Cookies["UIC"]["Name"];
                    loginModel.rememberMe = true;
                    loginModel.ReturnUrl = ReturnUrl;
                }
            }

            return PartialView(loginModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                IUserAuthenticationService services = new UserAuthenticationService();

                if (ModelState.IsValid)
                {
                    model.account = model.account.ToUpper();
                    model.password = model.password.ToUpper();
                    services.OnLogin(model);
                    var role = Request.Cookies["UIC"]["Role"];
                    LoginReturn result = new LoginReturn();
                    result.status = true;
                    if (!String.IsNullOrEmpty(model.ReturnUrl))
                    {
                        result.ReturnUrl = model.ReturnUrl;
                    }
                    else
                    {
                        result.ReturnUrl = "products";
                    }
                    return Json(result);
                }
                else
                {
                    ModelState.AddModelError("ErrorModelValid",
                        "您輸入的資料驗證不正確，請重新檢查。");
                    return Json("您輸入的資料驗證不正確，請重新檢查。", JsonRequestBehavior.AllowGet);
                }
            }
            catch (MyException ex)
            {
                ModelState.AddModelError("ErrorModelValid", ex.Message);
                return Json("帳號或密碼錯誤。", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        [AllowAnonymous]
        public ActionResult Index()
        {
            //var role = Request.Cookies["UIC"]["Role"];
            //return RedirectToAction("Index", role, new { area = role });
            return View();
        }
        #region LogOut 登出

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            IUserAuthenticationService services = new UserAuthenticationService();
            services.OnLogOut();
            return RedirectToAction("Products");
        }

        #endregion
        public ActionResult EChart()
        {
            return View();
        }
        #region 商品頁面(含詳細頁面)
        [AllowAnonymous]
        public ActionResult Section(int Id = 0, string name = null, string search = null, bool newproduct = false)
        {
            SectionService service = new SectionService();
            ViewBag.Id = Id;
            if (newproduct)
            {
                ViewBag.Category = "新商品一覽";
            }
            else
            {
                ViewBag.Category = (Id == 0) ? "全部傢俱" : service.GetCategory(Id);
            }
            ViewBag.NewProduct = newproduct;
            ViewBag.Tree = service.GetTree();
            ViewBag.Search = search;
            ViewBag.Name = name;
            return View();
        }
        [AllowAnonymous]
        public ActionResult SectionPartial(string name = "", int pstart = 0, int pend = 0, int Id = 0, int page = 1, bool newproduct = false)
        {
            int currentpage = page < 1 ? 1 : page;
            List<SectionModel> model = new List<SectionModel>();
            SectionService service = new SectionService();
            model = service.GetProductList(Id, name, pstart, pend, newproduct);
            ViewBag.NewProduct = newproduct;
            ViewBag.Id = Id;
            if (newproduct)
            {
                ViewBag.Category = "新商品一覽";
            }
            else
            {
                ViewBag.Category = (Id == 0) ? "全部傢俱" : service.GetCategory(Id);
            }
            ViewBag.Name = name;
            return PartialView(model.ToPagedList(currentpage, pageSize));
        }
        public JsonResult AddToCart(string ProductId, int? IsPush = null)
        {
            ShopCartService service = new ShopCartService();
            var res = service.AddToCart(ProductId, IsPush);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult ProductDetail(string ProductId, int? IsPush = null)
        {
            SectionService service = new SectionService();
            ViewBag.IsPush = IsPush;
            return View(service.GetProductDetail(ProductId));
        }
        [AllowAnonymous]
        public JsonResult RateStar(double star, string ProductId)
        {
            SectionService service = new SectionService();
            var result = service.RateStar(star, ProductId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public void ReplyTo(int Id, string ProductId, string replyContext)
        {
            SectionService service = new SectionService();
            service.ReplyTo(Id, ProductId, replyContext);
        }
        public void Reply(string ProductId, string replyContext)
        {
            SectionService service = new SectionService();
            service.Reply(ProductId, replyContext);
        }
        #endregion
        #region 進入首頁(含XX專區)
        [AllowAnonymous]
        public ActionResult Products()
        {
            SectionService service = new SectionService();
            ViewBag.Tree = service.GetTree();
            return View(service.GetIndex());
        }
        #endregion
        #region 最新消息
        [AllowAnonymous]
        public ActionResult News()
        {
            SectionService service = new SectionService();
            ViewBag.Tree = service.GetTree();
            return View(service.GetNews());
        }
        [AllowAnonymous]
        public ActionResult NewsDetail(int Id)
        {
            SectionService service = new SectionService();
            ViewBag.Tree = service.GetTree();
            return View(service.GetNewsDetail(Id));
        }
        #endregion
        #region 首頁
        [AllowAnonymous]
        public ActionResult TEST()
        {
            return View();
        }
        #endregion
        #region 註冊
        [AllowAnonymous]
        public ActionResult Regist()
        {
            RegistModel model = new RegistModel();
            model.Style = 1;
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Regist(RegistModel model)
        {
            RegistService service = new RegistService();
            service.Regist(model);
            LoginModel log = new LoginModel();
            log.account = model.Account.ToUpper();
            log.password = model.Password.ToUpper();
            IUserAuthenticationService logservices = new UserAuthenticationService();
            logservices.OnLogin(log);
            return RedirectToAction("Products");
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckAccount(string Account)
        {
            RegistService service = new RegistService();
            return Json(service.CheckAccount(Account), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 會員專區
        public ActionResult MemberSection()
        {
            MemberSectionService service = new MemberSectionService();
            var member = service.GetMember();
            return View(member);
        }
        [HttpPost]
        public ActionResult EditMember(MemberSectionModel model)
        {
            MemberSectionService service = new MemberSectionService();
            service.EditMember(model);
            return RedirectToAction("MemberSection");
        }
        public ActionResult ShopCart()
        {
            List<SectionModel> model = new List<SectionModel>();
            ShopCartService service = new ShopCartService();
            return View(service.GetCart(HttpContext.User.Identity.Name));
        }
        public void RemoveCartItem(string ProductId)
        {
            ShopCartService service = new ShopCartService();
            service.RemoveCartItem(ProductId);
        }
        public ActionResult Purchase()
        {
            List<SectionModel> model = new List<SectionModel>();
            ShopCartService service = new ShopCartService();
            ViewBag.PurchaseType = service.GetPurchaseType();
            return View(service.GetCart(HttpContext.User.Identity.Name));
        }
        public JsonResult ConfirmPurchase(string Quantity, string ProductId, string PurchaseStatus, string Total)
        {
            List<int> qty = JsonConvert.DeserializeObject<List<int>>(Quantity);
            List<string> pid = JsonConvert.DeserializeObject<List<string>>(ProductId);
            byte? status = Byte.Parse(PurchaseStatus);
            int total = Int32.Parse(Total.Split('元')[0]);
            ShopCartService service = new ShopCartService();
            PurchaseReturnModel res = new PurchaseReturnModel();
            res = service.ConfirmPurchase(qty, pid, status, total);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult History()
        {
            SectionService service = new SectionService();
            return View(service.GetHistory());
        }
        #endregion
        #region 後台

        #region 商品管理
        public ActionResult ProductIndex()
        {
            ProductService service = new ProductService();
            ViewBag.Head = "ARealShop商品管理";
            ViewBag.Ul = "商品管理";
            return View(service.Get());
        }

        public ActionResult ProductCreate()
        {
            TableService service = new TableService();
            ProductModel model = new ProductModel();
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate(ProductModel model)
        {
            ProductService service = new ProductService();
            service.Create(model);
            return RedirectToAction("ProductIndex");
        }
        #endregion

        #endregion
        #region 3D虛擬擺設
        [AllowAnonymous]
        public ActionResult FurnitureDecorate()
        {
            return PartialView();
        }
        #endregion
        #region APK連結
        [AllowAnonymous]
        public ActionResult Apk()
        {
            SectionService service = new SectionService();
            var url = service.GetApk();
            return Redirect(url);
        }
        #endregion
    }
}
