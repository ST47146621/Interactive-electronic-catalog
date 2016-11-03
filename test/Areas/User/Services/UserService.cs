using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Areas.User.Services
{
    public class UserService
    {
        DBEntity db = new DBEntity();

        public Member GetMember()
        {
            return db.Repository<Member>().Get(p => p.Account.Equals(HttpContext.Current.Request.Cookies["UIC"]["Name"])).FirstOrDefault();
        }
        public void EditUser(Member model)
        {
            var result = Mapper.DynamicMap<Member>(model);
            db.Repository<Member>().UpdateAll(result);
            db.Save();
        }
        public List<ShopCart> GetShopList()
        {
            return db.Repository<ShopCart>().Get(p => p.Account.Equals(HttpContext.Current.Request.Cookies["UIC"]["Name"])).OrderBy(p => p.Date).ToList();
        }
        public ShopCart GetShopCart(string ShopId)
        {
            return db.Repository<ShopCart>().Get(p => p.ShopId.Equals(ShopId)).FirstOrDefault();
        }
        public List<DriverLocation> GetDriverLocation(string ShopId)
        {
            return db.Repository<DriverLocation>().Get(p => p.ShopId.Equals(ShopId)).ToList();
        }
    }
}