using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public class TableService
    {
        private DBEntity db = new DBEntity();
        public BigModel Get()
        {
            BigModel model = new BigModel();
            model.a = db.Repository<Product>().Get().Select(p => new ProductModel()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ProductSpec = p.ProductSpec
            }).ToList();
            model.b = db.Repository<ShopCart>().Get().Select(p => new ShopCartModel()
            {
                Account = p.Account,
                Date = p.Date,
                Payment = p.Payment,
                Price = p.Price,
                ShopId = p.ShopId,
                Status = p.Status
            }).ToList();
            model.c = db.Repository<Member>().Get().Select(p => new MemberModel()
            {
                Account = p.Account,
                Birth = p.Birth.ToString().Substring(0, 10),
                Password = p.Password,
                Name = p.Name,
                Phone = p.Phone,
                Addr = p.Addr
            }).ToList();
            return model;
        }
        public ProductModel GetCreate()
        {
            ProductModel model = new ProductModel();
            model.PCategory = db.Repository<ProductCategory>().Get().ToList();
            model.PColor = db.Repository<Color>().Get().ToList();
            return model;
        }

    }
}