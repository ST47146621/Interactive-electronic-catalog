using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services.Generic;

namespace test.Services
{
    public class ColorService
    {
        DBEntity db = new DBEntity();
        public List<ColorModel> Get()
        {
            return db.Repository<Color>().Get().Select(p => new ColorModel()
            {
                Id = p.Id,
                Name = p.Color1,
                Sharp = p.Sharp
            }).ToList();
        }
        public void Create(ColorModel model)
        {
            var create = Mapper.DynamicMap<Color>(model);
            create.Color1 = model.Name;
            create.Sharp = model.Sharp;
            db.Repository<Color>().Create(create);
            db.Save();
        }
        public ColorNoValidModel GetEditData(int id)
        {
            ColorNoValidModel model = new ColorNoValidModel();
            model = db.Repository<Color>().Get(p => p.Id == id).Select(m => new ColorNoValidModel()
            {
                Name = m.Color1,
                Sharp = m.Sharp
            }).FirstOrDefault();
            return model;
        }
        public void Edit(ColorNoValidModel model)
        {
            var oldmodel = db.Repository<Color>().Get(p => p.Id == model.Id).FirstOrDefault();
            if (db.Repository<Color>().Get(p => p.Id == model.Id).Any())
            {
                oldmodel.Sharp = model.Sharp;
                oldmodel.Color1 = model.Name;
                db.Repository<Color>().UpdateAll(oldmodel);
                db.Save();
            }
        }
        public void Delete(int Id)
        {
            try
            {
                if (db.Repository<Color>().Get(p => p.Id == Id).Any())
                {
                    db.Repository<Color>().Delete(Id);
                    db.Save();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}