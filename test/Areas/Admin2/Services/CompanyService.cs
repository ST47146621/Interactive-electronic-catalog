using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services.Generic;

namespace test.Areas.Admin2.Services
{
    public class CompanyService
    {
        DBEntity db = new DBEntity();
        public List<CompanyModel> Get()
        {
            var result = db.Repository<Company>().Get().Select(p => new CompanyModel()
            {
                Addr=p.Addr,
                Img=p.Img,
                Name=p.Name,
                Phone=p.Phone,
                Sid=p.Sid
            }).ToList();
            return result;
        }
        public void Create(CompanyEditModel model)
        {
            var create = Mapper.DynamicMap<Company>(model);
            var filename = model.Sid;
            UploadFileHelper service = new UploadFileHelper() ;
            try
            {
                service.UploadFile(model.Img, filename);
                create.Img = filename;
                db.Repository<Company>().Create(create);
                db.Save();
            }
            catch
            {

            }
        }
        public CompanyEditModel GetEditData(string Sid)
        {
            CompanyEditModel model = new CompanyEditModel();
            model = db.Repository<Company>().Get(p => p.Sid == Sid).Select(m => new CompanyEditModel()
            {
                Sid=m.Sid,
                Addr=m.Addr,
                Name=m.Name,
                Phone=m.Phone
            }).FirstOrDefault();
            return model;
        }
        public void Edit(CompanyEditModel model)
        {
            CompanyModel viewmodel = new CompanyModel();
            var oldcompany=db.Repository<Company>().Get(p => p.Sid == model.Sid).AsQueryable();
            viewmodel.Sid = model.Sid;
            viewmodel.Phone = model.Phone;
            viewmodel.Img = oldcompany.FirstOrDefault().Img;
            viewmodel.Addr = model.Addr;
            viewmodel.Name = model.Name;
            try
            {
                var updatemodel = Mapper.DynamicMap<Company>(model);
                if (oldcompany.Any())
                {
                    UploadFileHelper service = new UploadFileHelper();
                    service.DeleteFiles(model.Sid);
                    service.UploadFile(model.Img, model.Sid);
                    updatemodel = new Company() { Sid = model.Sid };
                    db.Repository<Company>().UpdateWithViewModel<CompanyModel>(viewmodel, updatemodel);
                    db.Save();
                }
            }
            catch
            {

            }
        }
    }
}