using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Areas.User.Services;
using test.Extend;
using test.Models;
using test.Services;

namespace test.Areas.User.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("UserIndex", "User");
        }
        public ActionResult UserIndex()
        {
            UserService service = new UserService();
            ViewBag.Head = "ARealShop 使用者管理";
            return View(service.GetMember());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(Member model)
        {
            UserService service = new UserService();
            service.EditUser(model);
            return RedirectToAction("UserIndex");
        }
        public ActionResult ShopIndex()
        {
            UserService service = new UserService();
            ViewBag.Head = "ARealShop 使用者管理";
            return View(service.GetShopList());
        }
        public ActionResult ShopDetail(string ShopId)
        {
            UserService service = new UserService();
            return PartialView(service.GetShopCart(ShopId));
        }
        public ActionResult ShopLocation(string ShopId)
        {
            UserService service = new UserService();
            return PartialView(service.GetDriverLocation(ShopId));
        }
    }
}
