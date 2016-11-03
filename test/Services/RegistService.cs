using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public class RegistService
    {
        DBEntity db = new DBEntity();
        public bool Regist(RegistModel model)
        {
            try
            {
                model.Role = "User";
                model.Account=model.Account.ToUpper();
                model.Password=model.Password.ToUpper();
                Member result = new Member();
                result = Mapper.DynamicMap<Member>(model);
                db.Repository<Member>().Create(result);
                db.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool CheckAccount(string Account)
        {
            if (db.Repository<Member>().Get(p => p.Account == Account).Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}