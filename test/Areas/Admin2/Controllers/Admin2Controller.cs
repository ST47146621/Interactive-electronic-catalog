using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Areas.Admin2.Services;
using test.Extend;
using test.Models;
using test.Services;

namespace test.Areas.Admin2.Controllers
{
    [Authorize]
    public class Admin2Controller : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("MemberIndex", "Admin2");
        }
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

        #region 廠商管理
        public ActionResult CompanyIndex()
        {
            CompanyService service = new CompanyService();
            ViewBag.Head = "ARealShop廠商管理";
            ViewBag.Ul = "廠商管理";
            return View(service.Get());
        }
        public ActionResult CompanyCreate()
        {
            CompanyService service = new CompanyService();
            CompanyEditModel model = new CompanyEditModel();
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCreate(CompanyEditModel model)
        {
            CompanyService service = new CompanyService();
            service.Create(model);
            return RedirectToAction("CompanyIndex");
        }
        public ActionResult CompanyEdit(string Sid)
        {
            CompanyService service = new CompanyService();
            return PartialView(service.GetEditData(Sid));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyEdit(CompanyEditModel model)
        {
            CompanyService service = new CompanyService();
            service.Edit(model);
            return RedirectToAction("CompanyIndex");
        }
        #endregion
    }
}
