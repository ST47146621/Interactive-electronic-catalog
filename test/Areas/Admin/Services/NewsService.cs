using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services.Generic;

namespace test.Services
{
    public class NewsService
    {
        DBEntity db = new DBEntity();
        public List<NewsModel> Get()
        {
            var result = db.Repository<News>().Get().Select(p => new NewsModel()
            {
                Id = p.Id,
                Text = p.Text,
                Title = p.Title,
                Time = p.Time
            }).ToList();
            for (var i = 0; i < result.Count; i++)
            {
                result[i].Text = result[i].Text.Replace("<br>", "");
            }
            return result;
        }
        public void Create(NewsModel model)
        {
            UploadFileHelper upload_god = new UploadFileHelper();
            var currentid = db.Repository<News>().Get().OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefault() + 1;
            var create = Mapper.DynamicMap<News>(model);
            create.Title = model.Title;
            create.Text = model.Text.Replace("\r\n", "<br>");
            create.Time = DateTime.Now;
            create.ImageUrl = upload_god.UploadFile(model.ImgUrl, currentid.ToString());
            db.Repository<News>().Create(create);
            db.Save();
        }
        public NewsNoValidModel GetEditData(int id)
        {
            NewsNoValidModel model = new NewsNoValidModel();
            model = db.Repository<News>().Get(p => p.Id == id).Select(m => new NewsNoValidModel()
            {
                Title = m.Title,
                Text = m.Text,
                Time = m.Time,
                ImgUrl1 = m.ImageUrl
            }).FirstOrDefault();
            model.ImgUrl1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ImgUrl1;
            model.Text = model.Text.Replace("<br>", "\n");
            return model;
        }
        public void Edit(NewsNoValidModel model)
        {
            UploadFileHelper upload_god = new UploadFileHelper();
            var oldmodel = db.Repository<News>().Get(p => p.Id == model.Id).FirstOrDefault();
            if (db.Repository<News>().Get(p => p.Id == model.Id).Any())
            {

                oldmodel.Title = model.Title;
                oldmodel.Text = model.Text.Replace("\r\n", "<br>");
                if (model.ImgUrl != null)
                {
                    oldmodel.ImageUrl = upload_god.UploadFile(model.ImgUrl, oldmodel.ImageUrl.Split('.')[0]);
                }
                db.Repository<News>().UpdateAll(oldmodel);
                db.Save();
            }
        }
        public void Delete(int Id)
        {
            try
            {
                if (db.Repository<News>().Get(p => p.Id == Id).Any())
                {
                    db.Repository<News>().Delete(Id);
                    db.Save();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}