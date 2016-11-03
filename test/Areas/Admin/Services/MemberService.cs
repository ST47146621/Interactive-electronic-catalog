using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public class MemberService
    {
        DBEntity db = new DBEntity();
        public List<MemberModel> Get()
        {
            var result = db.Repository<Member>().Get().Select(p => new MemberModel()
            {
                Account = p.Account,
                Birth = p.Birth.ToString(),
                Password = p.Password,
                Name = p.Name,
                Phone = p.Phone,
                Addr = p.Addr
            }).ToList();
            for (var i = 0; i < result.Count; i++)
            {
                DateTime temp =DateTime.Parse(result[i].Birth);
                result[i].Birth = temp.ToString("yyyy-MM-dd");
            }
            return result;
        }
        public void Create(MemberModel model)
        {
            var create = Mapper.DynamicMap<Member>(model);
            create.Role = "User";
            db.Repository<Member>().Create(create);
            db.Save();
        }
        public MemberEditModel GetEditData(string Id)
        {
            MemberEditModel model = new MemberEditModel();
            model = db.Repository<Member>().Get(p => p.Account == Id).Select(m => new MemberEditModel()
            {
                Account=m.Account,
                Addr=m.Addr,
                Birth=m.Birth,
                Name=m.Name,
                Password=m.Password,
                Phone=m.Phone
            }).FirstOrDefault();
            return model;
        }
        public void Edit(MemberEditModel model)
        {
            var updatemodel = Mapper.DynamicMap<Member>(model);
            if (db.Repository<Member>().Get(p => p.Account == model.Account).Any())
            {
                updatemodel = new Member() { Account = model.Account };
                db.Repository<Member>().UpdateWithViewModel<MemberEditModel>(model, updatemodel);
                db.Save();
            }
        }
        public void Delete(string Id)
        {
            if (db.Repository<Member>().Get(p => p.Account == Id).Any())
            {
                db.Repository<Member>().Delete(Id);
                db.Save();
            }
        }
    }
}