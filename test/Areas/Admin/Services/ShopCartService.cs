using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Services
{
    public class ShopCartService
    {
        DBEntity db = new DBEntity();
        public List<ShopCartModel> Get()
        {
            return db.Repository<ShopCart>().Get().Select(p => new ShopCartModel()
            {
                Account = p.Account,
                Date = p.Date,
                Payment = p.Payment,
                Price = p.Price,
                ShopId = p.ShopId,
                Status = p.Status
            }).ToList();
        }
        public ShopCartDetailModel GetDetail(string Id)
        {
            return db.Repository<ShopCart>().Get(p => p.ShopId == Id).Select(p => new ShopCartDetailModel()
            {
                Account = p.Account,
                Name = p.Member.Name,
                Date = p.Date,
                Payment = p.PurchaseStatus.Name,
                Price = p.Price,
                ShopId = p.ShopId,
                Status = p.Status
            }).FirstOrDefault();
        }
        public List<SectionModel> GetCart(string Account)
        {
            var query = db.Repository<ShopCart>().Get(p => p.Account == Account && p.Status == false).AsQueryable();
            List<SectionModel> model = new List<SectionModel>();
            var shopid = query.Select(p => p.ShopId).FirstOrDefault();
            model = db.Repository<ShopCartList>().Get(p => p.ShopId == shopid).Select(p => new SectionModel()
            {
                ProductId = p.Product.ProductId,
                ProductName = p.Product.ProductName,
                ProductImgUrl1 = p.Product.ProductImgUrl1,
                ProductImgUrl2 = p.Product.ProductImgUrl2,
                ProductImgUrl3 = p.Product.ProductImgUrl3,
                ProductImgUrl4 = p.Product.ProductImgUrl4,
                ProductMemo1 = p.Product.ProductMemo1,
                ProductPrice = p.Product.ProductPrice
            }).ToList();
            for (var i = 0; i < model.Count; i++)
            {
                model[i].ProductMemo1 = model[i].ProductMemo1.Replace("<br>", "");
                if (model[i].ProductMemo1.Length > 16)
                {
                    model[i].ProductMemo1 = model[i].ProductMemo1.Substring(0, 16) + "...";
                }
                model[i].ProductImgUrl1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model[i].ProductImgUrl1;
                if (model[i].ProductImgUrl2 != null)
                {
                    model[i].ProductImgUrl2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model[i].ProductImgUrl2;
                }
                if (model[i].ProductImgUrl3 != null)
                {
                    model[i].ProductImgUrl3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model[i].ProductImgUrl3;
                }
                if (model[i].ProductImgUrl4 != null)
                {
                    model[i].ProductImgUrl4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model[i].ProductImgUrl4;
                }
                var productid = model[i].ProductId;
                var sharp = db.Repository<ProductColor>().Get(p => p.ProductId == productid).Select(p => p.Color1.Sharp).ToList();
                model[i].Sharp = new List<string>();
                model[i].Sharp = sharp;
            }
            return model;
        }
        public void RemoveCartItem(string ProductId)
        {
            var Account = HttpContext.Current.Request.Cookies["UIC"]["Name"];
            var query = db.Repository<ShopCart>().Get(p => p.Account == Account && p.Status == false).AsQueryable();
            var shopid = query.Select(p => p.ShopId).FirstOrDefault();
            var deleteitem = db.Repository<ShopCartList>().Get(p => p.ShopId == shopid && p.ProductId == ProductId).FirstOrDefault();
            db.Repository<ShopCartList>().Delete(deleteitem);
            db.Save();
        }
        public List<SelectListItem> GetPurchaseType()
        {
            var status = db.Repository<PurchaseStatus>().Get().ToList();
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (var item in status)
            {
                result.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            return result;
        }
        public PurchaseReturnModel ConfirmPurchase(List<int> Quantity, List<string> ProductId, byte? PurchaseStatus, int Total)
        {
            PurchaseReturnModel result = new PurchaseReturnModel();
            result.ProductStatus = new List<ProductParchaseStatus>();
            result.Status = true;
            try
            {
                for (var i = 0; i < ProductId.Count; i++)
                {
                    var chkpid = ProductId[i];
                    var chkqty = Quantity[i];
                    var pidquery = db.Repository<Product>().Get(p => p.ProductId == chkpid);
                    var chkonshelf = pidquery.Select(p => p.OnShelf).FirstOrDefault();
                    var currentqty = pidquery.Select(p => p.Quantity).FirstOrDefault();
                    if (chkonshelf == false)
                    {
                        result.Status = false;
                        result.ProductStatus.Add(new ProductParchaseStatus() { ProductName = pidquery.Select(p => p.ProductName).FirstOrDefault(), ProductStatus = "產品已下架!!" });
                    }
                    else if (chkqty > currentqty)
                    {
                        result.Status = false;
                        result.ProductStatus.Add(new ProductParchaseStatus() { ProductName = pidquery.Select(p => p.ProductName).FirstOrDefault(), ProductStatus = "庫存不足" + (chkqty - currentqty) + "個" });
                    }
                }
                if (result.Status)
                {
                    var Account = HttpContext.Current.Request.Cookies["UIC"]["Name"];
                    var query = db.Repository<ShopCart>().Get(p => p.Account == Account && p.Status == false).AsQueryable();
                    var shopid = query.Select(p => p.ShopId).FirstOrDefault();
                    for (var i = 0; i < ProductId.Count; i++)
                    {
                        var productid = ProductId[i];
                        var updatecartitem = db.Repository<ShopCartList>().Get(p => p.ProductId == productid && p.ShopId == shopid).FirstOrDefault();
                        updatecartitem.Quantity = Quantity[i];
                        db.Repository<ShopCartList>().UpdateAll(updatecartitem);
                        db.Save();
                        var ProductQuantityUpdate = db.Repository<Product>().Get(p => p.ProductId == productid).FirstOrDefault();
                        ProductQuantityUpdate.Quantity -= Quantity[i];
                        db.Repository<Product>().UpdateAll(ProductQuantityUpdate);
                        db.Save();
                    }
                    var shopcartModel = db.Repository<ShopCart>().Get(p => p.ShopId == shopid).FirstOrDefault();
                    shopcartModel.Price = Total;
                    shopcartModel.Status = true;
                    shopcartModel.Payment = PurchaseStatus;
                    shopcartModel.Date = DateTime.Now;
                    db.Repository<ShopCart>().UpdateAll(shopcartModel);
                    db.Save();
                }
            }
            catch (Exception e)
            {
                result.Status = false;
                result.Exception = true;
            }
            return result;
        }
        public bool AddToCart(string ProductId, int? IsPush = null)
        {
            
            var Account = HttpContext.Current.Request.Cookies["UIC"]["Name"];
            var iscart = db.Repository<ShopCart>().Get(p => p.Account == Account && p.Status == false).Select(p => new ShopStatus() { ShopId = p.ShopId, Status = p.Status }).FirstOrDefault();
            var getprice = db.Repository<Product>().Get(p => p.ProductId == ProductId).Select(p => p.ProductPrice).FirstOrDefault();
            int price = Int32.Parse(getprice.ToString());
            if (iscart == null)
            {
                string today = DateTime.Now.ToString("yyyyMMdd");
                string newshopid;
                var shopidlist = db.Repository<ShopCart>()
            .Get(p=>p.ShopId.Contains(today)).Select(p => p.ShopId).AsQueryable();
                List<int> newshopidlist = new List<int>();
                if (!shopidlist.Any())
                {
                    newshopid = string.Format("{0}{1}", today, 1.ToString("000"));
                }
                else
                {
                    foreach (var item in shopidlist.ToList())
                        newshopidlist.Add(int.Parse(item.Substring(9)));

                    newshopid = string.Format("{0}{1}", today, (newshopidlist.Max() + 1).ToString("000"));
                }
                ShopCart model = new ShopCart();
                model.Account = Account;
                model.ShopId = newshopid;
                model.Status = false;
                model.Price = (price);
                db.Repository<ShopCart>().Create(model);
                db.Save();

                ShopCartList listmodel = new ShopCartList();
                listmodel.Id = 1;
                listmodel.ShopId = newshopid;
                listmodel.ProductId = ProductId;
                listmodel.Quantity = 1;
                listmodel.IsPush = IsPush;
                db.Repository<ShopCartList>().Create(listmodel);
                db.Save();
            }
            else
            {
                var model = new ShopCart() { ShopId = iscart.ShopId, Status = iscart.Status };
                db.Repository<ShopCart>().Attach(model);
                model.Price = db.Repository<ShopCart>().Get(p => p.ShopId == iscart.ShopId && p.Status == iscart.Status).Select(p => p.Price).FirstOrDefault() + (price);

                var isproduct = db.Repository<ShopCartList>().Get(p => p.ShopId == iscart.ShopId && p.ProductId == ProductId).Select(p => new ShopListId()
                {
                    Id = p.Id,
                    Quantity = p.Quantity,
                    ShopId = p.ShopId
                }).FirstOrDefault();
                if (isproduct == null)
                {
                    var quantity = db.Repository<ShopCartList>().Get(p => p.ShopId == iscart.ShopId).Select(p => p.Id).AsQueryable();
                    List<int> newquantitylist = new List<int>();
                    int newquantity;
                    foreach (var item in quantity.ToList())
                        newquantitylist.Add(item);
                    if (newquantitylist.Count == 0)
                    {
                        newquantity = 1;
                    }
                    else
                    {
                        newquantity = newquantitylist.Last() + 1;
                    }
                    ShopCartList listmodel = new ShopCartList();
                    listmodel.Id = newquantity;
                    listmodel.ShopId = iscart.ShopId;
                    listmodel.ProductId = ProductId;
                    listmodel.Quantity = 1;
                    listmodel.IsPush = IsPush;
                    db.Repository<ShopCartList>().Create(listmodel);
                }
                else
                {
                    var listmodel = new ShopCartList() { ShopId = isproduct.ShopId, Quantity = isproduct.Quantity, Id = isproduct.Id };
                    db.Repository<ShopCartList>().Attach(listmodel);
                    listmodel.Quantity = listmodel.Quantity + 1;
                    listmodel.IsPush = IsPush;
                }
                db.Save();

            }
            return true;
        }
        public List<ShopHistoryModel> GetHistory()
        {
            var Account = HttpContext.Current.Request.Cookies["UIC"]["Name"];

            List<ShopHistoryModel> model = new List<ShopHistoryModel>();
            List<ShopHistoryItemModel> itemmodel = new List<ShopHistoryItemModel>();
            var shopcart = db.Repository<ShopCart>().Get(p => p.Account == Account).ToList();
            for (var i = 0; i < shopcart.Count; i++)
            {
                ShopHistoryModel lit = new ShopHistoryModel();
                if (shopcart[i].Date.ToString().Equals(""))
                {
                    lit.Date = "";
                }
                else
                {
                    lit.Date = DateTime.Parse(shopcart[i].Date.ToString()).ToString("yyyy/MM/dd");
                }
                lit.Price = shopcart[i].Price;
                if (shopcart[i].Payment == null)
                {
                    lit.Payment = "";
                }
                else
                {
                    lit.Payment = shopcart[i].PurchaseStatus.Name;
                }

                lit.ShopId = shopcart[i].ShopId;
                lit.Status = shopcart[i].Status;
                if (lit.Status == true)
                {
                    lit.PayStatus = "已付款";
                }
                else
                {
                    lit.PayStatus = "未付款";
                }

                var shopid = shopcart[i].ShopId;
                var cartlist = db.Repository<ShopCartList>().Get(p => p.ShopId == shopid).ToList();
                itemmodel = new List<ShopHistoryItemModel>();
                for (var j = 0; j < cartlist.Count; j++)
                {
                    ShopHistoryItemModel lim = new ShopHistoryItemModel();
                    lim.Price = cartlist[j].Product.ProductPrice * cartlist[j].Quantity;
                    lim.ProductId = cartlist[j].ProductId;
                    lim.ProductName = cartlist[j].Product.ProductName;
                    lim.Quantity = cartlist[j].Quantity;
                    itemmodel.Add(lim);
                }
                lit.item = itemmodel;
                model.Add(lit);
            }
            return model;
        }
    }
}