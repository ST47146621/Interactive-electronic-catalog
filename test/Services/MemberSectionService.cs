using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public class MemberSectionService
    {
        DBEntity db = new DBEntity();
        public MemberSectionModel GetMember()
        {
            var account=HttpContext.Current.Request.Cookies["UIC"]["Name"];
            var getcol = db.Repository<Member>().Get(p => p.Account == account).Select(p => new RegistModel()
            {
                Account = p.Account,
                Addr = p.Addr,
                Birth = p.Birth,
                Job = p.Job,
                Sex = p.Sex,
                Style = p.Style,
                Money = p.Money,
                Name = p.Name,
                Phone = p.Phone
            }).FirstOrDefault();
            MemberSectionModel model = new MemberSectionModel();
            model.Account = getcol.Account;
            model.Addr = getcol.Addr;
            model.Birth = getcol.Birth.ToString("yyyy/MM/dd");
            model.Job = (getcol.Job == 0) ? "國小/國中" : (getcol.Job == 1) ? "高中生/五專生" : (getcol.Job == 2) ? "大學生" : (getcol.Job == 3) ? "上班族" : "自營業";
            model.Sex = (getcol.Sex == true) ? "男性" : "女性";
            model.Style = getcol.Style;
            model.Money = (getcol.Money == 0) ? "無收入" : (getcol.Money == 1) ? "20000以下" : (getcol.Money == 2) ? "20000~50000" : "50000以上";
            model.Name = getcol.Name;
            model.Phone = getcol.Phone;
            return model;
        }
        public void EditMember(MemberSectionModel model)
        {
            Member result = new Member();
            var account = HttpContext.Current.Request.Cookies["UIC"]["Name"];
            result = db.Repository<Member>().Get(p => p.Account == account).FirstOrDefault();
            result.Name = model.Name;
            if (!String.IsNullOrEmpty(model.Password))
            {
                result.Password = model.Password.ToUpper();
            }
            result.Addr = model.Addr;
            result.Phone = model.Phone;
            result.Style = model.Style;
            db.Repository<Member>().UpdateAll(result);
            db.Save();
        }
    }
}