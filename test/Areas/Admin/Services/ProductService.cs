using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services.Generic;

namespace test.Services
{
    public class ProductService
    {
        DBEntity db = new DBEntity();
        public List<ProductModel> Get()
        {
            return db.Repository<Product>().Get().Select(p => new ProductModel()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ProductSpec = p.ProductSpec
            }).ToList();
        }
        public void Create(ProductModel model)
        {
            var create = Mapper.DynamicMap<Product>(model);
            UploadFileHelper upload_god = new UploadFileHelper();
            if (model.ProductImgUrl1 != null)
            {
                create.ProductImgUrl1 = upload_god.UploadFile(model.ProductImgUrl1, model.ProductId + "1");
            }
            if (model.ProductImgUrl2 != null)
            {
                create.ProductImgUrl2 = upload_god.UploadFile(model.ProductImgUrl2, model.ProductId + "2");
            }
            if (model.ProductImgUrl3 != null)
            {
                create.ProductImgUrl3 = upload_god.UploadFile(model.ProductImgUrl3, model.ProductId + "3");
            }
            if (model.ProductImgUrl4 != null)
            {
                create.ProductImgUrl4 = upload_god.UploadFile(model.ProductImgUrl4, model.ProductId + "4");
            }
            create.ProductMemo1 = model.ProductMemo.Replace("\r\n", "<br>");
            create.ProductSpec = model.ProductSpec.Replace("\r\n", "<br>");
            create.Time = DateTime.Now;
            db.Repository<Product>().Create(create);
            db.Save();
            if (model.SetCategory != null)
            {
                for (var i = 0; i < model.SetCategory.Length; i++)
                {
                    CategoryTable categorytable = new CategoryTable();
                    categorytable.Id = model.SetCategory[i];
                    categorytable.ProductId = model.ProductId;
                    db.Repository<CategoryTable>().Create(categorytable);

                }
            }
            if (model.SetColor != null)
            {
                for (var i = 0; i < model.SetColor.Length; i++)
                {
                    ProductColor productcolor = new ProductColor();
                    productcolor.ProductId = model.ProductId;
                    productcolor.Color = model.SetColor[i];
                    db.Repository<ProductColor>().Create(productcolor);
                }
            }
            db.Save();
        }
        public ProductNoValidModel GetEditData(string Id)
        {
            ProductNoValidModel model = new ProductNoValidModel();
            model = db.Repository<Product>().Get(p => p.ProductId == Id).Select(m => new ProductNoValidModel()
            {
                ProductId = m.ProductId,
                ProductPrice = m.ProductPrice,
                ProductSpec = m.ProductSpec,
                ProductName = m.ProductName,
                ProductMemo = m.ProductMemo1,
                ImgUrl1 = m.ProductImgUrl1,
                ImgUrl2 = m.ProductImgUrl2,
                ImgUrl3 = m.ProductImgUrl3,
                ImgUrl4 = m.ProductImgUrl4,
                ProductCost = m.ProductCost,
                PushHot = m.PushHot,
                Quantity = m.Quantity,
                OnShelf = m.OnShelf
            }).FirstOrDefault();
            model.ImgUrl1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ImgUrl1;
            if (model.ImgUrl2 != null)
            {
                model.ImgUrl2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ImgUrl2;
            }
            if (model.ImgUrl3 != null)
            {
                model.ImgUrl3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ImgUrl3;
            }
            if (model.ImgUrl4 != null)
            {
                model.ImgUrl4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ImgUrl4;
            }
            model.ProductMemo = model.ProductMemo.Replace("<br>", "\n");
            model.ProductSpec = model.ProductSpec.Replace("<br>", "\n");

            List<ProductCategoryEditModel> categoryeditmodel = new List<ProductCategoryEditModel>();
            List<ColorEditModel> coloreditmodel = new List<ColorEditModel>();
            var allcategory = db.Repository<ProductCategory>().Get().ToList();
            var allcolor = db.Repository<Color>().Get().ToList();
            for (var i = 0; i < allcategory.Count; i++)//所有商品類別
            {
                var nowcategory = allcategory[i].Id;
                if (db.Repository<CategoryTable>().Get(p => p.ProductId == Id && p.Id == nowcategory).Any())//如果這個商品有在類別裡
                {
                    categoryeditmodel.Add(new ProductCategoryEditModel() { Id = allcategory[i].Id, check = true, Name = allcategory[i].Name });
                }
                else
                {
                    categoryeditmodel.Add(new ProductCategoryEditModel() { Id = allcategory[i].Id, check = false, Name = allcategory[i].Name });
                }
            }
            for (var i = 0; i < allcolor.Count; i++)
            {
                var nowcolor = allcolor[i].Id;
                if (db.Repository<ProductColor>().Get(p => p.ProductId == Id && p.Color == nowcolor).Any())
                {
                    coloreditmodel.Add(new ColorEditModel() { Id = allcolor[i].Id, Name = allcolor[i].Color1, check = true });
                }
                else
                {
                    coloreditmodel.Add(new ColorEditModel() { Id = allcolor[i].Id, Name = allcolor[i].Color1, check = false });
                }
            }
            model.PCategory = new List<ProductCategoryEditModel>();
            model.PCategory = categoryeditmodel;
            model.PColor = new List<ColorEditModel>();
            model.PColor = coloreditmodel;
            return model;
        }
        public void Edit(ProductNoValidModel model)
        {
            try 
            {
                var oldmodel = db.Repository<Product>().Get(p => p.ProductId == model.ProductId).FirstOrDefault();
                UploadFileHelper upload_god = new UploadFileHelper();

                if (db.Repository<Product>().Get(p => p.ProductId == model.ProductId).Any())
                {
                    if (model.ProductImgUrl1 != null)
                    {
                        upload_god.DeleteFiles(oldmodel.ProductImgUrl1);
                        oldmodel.ProductImgUrl1 = upload_god.UploadFile(model.ProductImgUrl1, model.ProductId + "1");
                    }
                    if (model.ProductImgUrl2 != null)
                    {
                        upload_god.DeleteFiles(oldmodel.ProductImgUrl2);
                        oldmodel.ProductImgUrl2 = upload_god.UploadFile(model.ProductImgUrl2, model.ProductId + "2");
                    }
                    if (model.ProductImgUrl3 != null)
                    {
                        upload_god.DeleteFiles(oldmodel.ProductImgUrl3);
                        oldmodel.ProductImgUrl3 = upload_god.UploadFile(model.ProductImgUrl3, model.ProductId + "3");
                    }
                    if (model.ProductImgUrl4 != null)
                    {
                        upload_god.DeleteFiles(oldmodel.ProductImgUrl4);
                        oldmodel.ProductImgUrl4 = upload_god.UploadFile(model.ProductImgUrl4, model.ProductId + "4");
                    }
                    oldmodel.ProductName = model.ProductName;
                    oldmodel.ProductMemo1 = model.ProductMemo.Replace("\r\n", "<br>");
                    oldmodel.ProductSpec = model.ProductSpec.Replace("\r\n", "<br>");
                    oldmodel.ProductCost = Int32.Parse(model.ProductCost.ToString());
                    oldmodel.ProductPrice = Int32.Parse(model.ProductPrice.ToString());
                    oldmodel.PushHot = model.PushHot;
                    oldmodel.Quantity = model.Quantity;
                    oldmodel.OnShelf = model.OnShelf;
                    db.Repository<Product>().UpdateAll(oldmodel);
                    db.Save();
                    //刪除舊資料
                    var productid = model.ProductId;
                    var oldcategory = db.Repository<CategoryTable>().Get(p => p.ProductId == productid).ToList();
                    var oldcolor = db.Repository<ProductColor>().Get(p => p.ProductId == productid).ToList();
                    for (var i = 0; i < oldcategory.Count; i++)
                    {
                        db.Repository<CategoryTable>().Delete(oldcategory[i]);
                    }
                    for (var i = 0; i < oldcolor.Count; i++)
                    {
                        db.Repository<ProductColor>().Delete(oldcolor[i]);
                    }
                    db.Save();
                    //新增關聯
                    if (model.SetCategory != null)
                    {
                        for (var i = 0; i < model.SetCategory.Length; i++)
                        {
                            CategoryTable categorytable = new CategoryTable();
                            categorytable.Id = model.SetCategory[i];
                            categorytable.ProductId = model.ProductId;
                            db.Repository<CategoryTable>().Create(categorytable);

                        }
                    }
                    if (model.SetColor != null)
                    {
                        for (var i = 0; i < model.SetColor.Length; i++)
                        {
                            ProductColor productcolor = new ProductColor();
                            productcolor.ProductId = model.ProductId;
                            productcolor.Color = model.SetColor[i];
                            db.Repository<ProductColor>().Create(productcolor);
                        }
                    }
                    db.Save();
                }
            }
            catch (Exception e)
            {

            }
            
        }
        public void Delete(string Id)
        {
            try
            {
                if (db.Repository<Product>().Get(p => p.ProductId == Id).Any())
                {
                    db.Repository<Product>().Delete(Id);
                    db.Save();
                }
            }
            catch (Exception e)
            {

            }
        }
        public bool CheckProductId(string ProductId)
        {
            bool getProductId = db.Repository<Product>().Get(p => p.ProductId == ProductId).Any();
            return !getProductId;
        }
    }
}