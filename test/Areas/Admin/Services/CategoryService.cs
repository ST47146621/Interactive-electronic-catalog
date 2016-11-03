using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services.Generic;

namespace test.Services
{
    public class CategoryService
    {
        DBEntity db = new DBEntity();
        public List<CategoryModel> Get()
        {
            var result = db.Repository<ProductCategory>().Get().Select(p => new CategoryModel()
            {
                Id = p.Id,
                Name = p.Name,
                Summary = p.Summary
            }).ToList();
            return result;
        }
        public void Create(CategoryModel model)
        {
            var create = Mapper.DynamicMap<ProductCategory>(model);
            UploadFileHelper upload_god = new UploadFileHelper();
            if (model.ImgUrl != null)
            {
                create.ImageUrl = upload_god.UploadFile(model.ImgUrl, model.Name);
            }
            create.Name = model.Name;
            create.Summary = model.Summary.Replace("\r\n", "<br>");
            db.Repository<ProductCategory>().Create(create);
            db.Save();
        }
        public CategoryNoValidModel GetEditData(int id)
        {
            CategoryNoValidModel model = new CategoryNoValidModel();
            model = db.Repository<ProductCategory>().Get(p => p.Id == id).Select(m => new CategoryNoValidModel()
            {
                ImgUrl1 = m.ImageUrl,
                Summary = m.Summary,
                Name = m.Name
            }).FirstOrDefault();
            model.ImgUrl1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ImgUrl1;
            model.Name = model.Name;
            model.Summary = model.Summary.Replace("<br>", "\n");
            return model;
        }
        public void Edit(CategoryNoValidModel model)
        {
            var oldmodel = db.Repository<ProductCategory>().Get(p => p.Id == model.Id).FirstOrDefault();
            UploadFileHelper upload_god = new UploadFileHelper();

            if (db.Repository<ProductCategory>().Get(p => p.Id == model.Id).Any())
            {
                if (model.ImgUrl != null)
                {
                    upload_god.DeleteFiles(oldmodel.ImageUrl);
                    oldmodel.ImageUrl = upload_god.UploadFile(model.ImgUrl, model.Name);
                }
                oldmodel.Name = model.Name;
                oldmodel.Summary = model.Summary;
                db.Repository<ProductCategory>().UpdateAll(oldmodel);
                db.Save();
            }
        }
        public void Delete(int Id)
        {
            try
            {
                if (db.Repository<ProductCategory>().Get(p => p.Id == Id).Any())
                {
                    db.Repository<ProductCategory>().Delete(Id);
                    db.Save();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}