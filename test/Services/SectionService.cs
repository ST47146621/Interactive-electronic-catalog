using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public class SectionService
    {
        DBEntity db = new DBEntity();
        public List<SectionModel> GetProductList(int Id, string name, int pstart, int pend, bool newproduct)
        {
            List<SectionModel> model = new List<SectionModel>();
            if (newproduct)
            {
                #region 最新商品
                var getTime = DateTime.Now;
                var startTime = getTime.AddDays(-7);
                var all = db.Repository<Product>().Get(p => p.Time >= startTime && p.Time <= getTime).OrderByDescending(p => p.Time).Select(p => new SectionModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductImgUrl1 = p.ProductImgUrl1,
                    ProductImgUrl2 = p.ProductImgUrl2,
                    ProductImgUrl3 = p.ProductImgUrl3,
                    ProductImgUrl4 = p.ProductImgUrl4,
                    ProductMemo1 = p.ProductMemo1,
                    ProductPrice = p.ProductPrice
                }).AsQueryable();

                if (!String.IsNullOrEmpty(name))
                {
                    all = all.Where(p => p.ProductName.Contains(name));
                }
                if (pend > 0)
                {
                    all = all.Where(p => p.ProductPrice >= pstart && p.ProductPrice <= pend);
                }
                var result = all.ToList();
                for (var i = 0; i < result.Count; i++)
                {
                    result[i].ProductMemo1 = result[i].ProductMemo1.Replace("<br>", "");
                    if (result[i].ProductMemo1.Length > 16)
                    {
                        result[i].ProductMemo1 = result[i].ProductMemo1.Substring(0, 16) + "...";
                    }
                    result[i].ProductImgUrl1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl1;
                    if (result[i].ProductImgUrl2 != null)
                    {
                        result[i].ProductImgUrl2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl2;
                    }
                    if (result[i].ProductImgUrl3 != null)
                    {
                        result[i].ProductImgUrl3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl3;
                    }
                    if (result[i].ProductImgUrl4 != null)
                    {
                        result[i].ProductImgUrl4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl4;
                    }
                    var productid = result[i].ProductId;
                    var sharp = db.Repository<ProductColor>().Get(p => p.ProductId == productid).Select(p => p.Color1.Sharp).ToList();
                    result[i].Sharp = new List<string>();
                    result[i].Sharp = sharp;
                }
                return result;

                #endregion
            }
            else
            {

            }
            #region 一般商品(沒有最新之分)
            if (Id == 0) //全部商品
            {
                var all = db.Repository<Product>().Get().Select(p => new SectionModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductImgUrl1 = p.ProductImgUrl1,
                    ProductImgUrl2 = p.ProductImgUrl2,
                    ProductImgUrl3 = p.ProductImgUrl3,
                    ProductImgUrl4 = p.ProductImgUrl4,
                    ProductMemo1 = p.ProductMemo1,
                    ProductPrice = p.ProductPrice
                }).AsQueryable();

                if (!String.IsNullOrEmpty(name))
                {
                    all = all.Where(p => p.ProductName.Contains(name));
                }
                if (pend > 0)
                {
                    all = all.Where(p => p.ProductPrice >= pstart && p.ProductPrice <= pend);
                }
                var result = all.ToList();
                for (var i = 0; i < result.Count; i++)
                {
                    result[i].ProductMemo1 = result[i].ProductMemo1.Replace("<br>", "");
                    if (result[i].ProductMemo1.Length > 16)
                    {
                        result[i].ProductMemo1 = result[i].ProductMemo1.Substring(0, 16) + "...";
                    }
                    result[i].ProductImgUrl1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl1;
                    if (result[i].ProductImgUrl2 != null)
                    {
                        result[i].ProductImgUrl2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl2;
                    }
                    if (result[i].ProductImgUrl3 != null)
                    {
                        result[i].ProductImgUrl3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl3;
                    }
                    if (result[i].ProductImgUrl4 != null)
                    {
                        result[i].ProductImgUrl4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl4;
                    }
                    var productid = result[i].ProductId;
                    var sharp = db.Repository<ProductColor>().Get(p => p.ProductId == productid).Select(p => p.Color1.Sharp).ToList();
                    result[i].Sharp = new List<string>();
                    result[i].Sharp = sharp;
                }
                return result;
            }
            else //分類商品
            {
                var categorylist = db.Repository<CategoryTable>().Get(p => p.Id == Id).Select(p => p.ProductId).ToList();
                var all = db.Repository<Product>().Get().Select(p => new SectionModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductImgUrl1 = p.ProductImgUrl1,
                    ProductImgUrl2 = p.ProductImgUrl2,
                    ProductImgUrl3 = p.ProductImgUrl3,
                    ProductImgUrl4 = p.ProductImgUrl4,
                    ProductMemo1 = p.ProductMemo1,
                    ProductPrice = p.ProductPrice
                }).AsQueryable();
                if (!String.IsNullOrEmpty(name))
                {
                    all = all.Where(p => p.ProductName.Contains(name));
                }
                if (pend > 0)
                {
                    all = all.Where(p => p.ProductPrice >= pstart && p.ProductPrice <= pend);
                }
                var allproduct = all.ToList();

                List<SectionModel> result = new List<SectionModel>();
                foreach (var allpro in allproduct)
                {
                    foreach (var catepro in categorylist)
                    {
                        if (allpro.ProductId == catepro)
                        {
                            result.Add(allpro);
                            break;
                        }
                    }
                }
                for (var i = 0; i < result.Count; i++)
                {
                    result[i].ProductMemo1 = result[i].ProductMemo1.Replace("<br>", "");
                    if (result[i].ProductMemo1.Length > 16)
                    {
                        result[i].ProductMemo1 = result[i].ProductMemo1.Substring(0, 16) + "...";
                    }
                    result[i].ProductImgUrl1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl1;
                    if (result[i].ProductImgUrl2 != null)
                    {
                        result[i].ProductImgUrl2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl2;
                    }
                    if (result[i].ProductImgUrl3 != null)
                    {
                        result[i].ProductImgUrl3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl3;
                    }
                    if (result[i].ProductImgUrl4 != null)
                    {
                        result[i].ProductImgUrl4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + result[i].ProductImgUrl4;
                    }
                    var productid = result[i].ProductId;
                    var sharp = db.Repository<ProductColor>().Get(p => p.ProductId == productid).Select(p => p.Color1.Sharp).ToList();
                    result[i].Sharp = new List<string>();
                    result[i].Sharp = sharp;
                }
                return result.ToList();
            }
            #endregion


        }
        public string GetCategory(int ProductCategory)
        {
            return db.Repository<ProductCategory>().Get(p => p.Id == ProductCategory).Select(p => p.Name).FirstOrDefault();
        }
        public List<ProductCategory> GetTree()
        {
            return db.Repository<ProductCategory>().Get().ToList();
        }
        public List<HistoryModel> GetHistory()
        {
            var Account = HttpContext.Current.Request.Cookies["UIC"]["Name"];

            List<HistoryModel> model = new List<HistoryModel>();
            List<HistoryItemModel> itemmodel = new List<HistoryItemModel>();
            var shopcart = db.Repository<ShopCart>().Get(p => p.Account == Account && p.Status == true).ToList();
            for (var i = 0; i < shopcart.Count; i++)
            {
                HistoryModel lit = new HistoryModel();
                lit.Date = DateTime.Parse(shopcart[i].Date.ToString()).ToString("yyyy/MM/dd");
                lit.Price = shopcart[i].Price;
                lit.Payment = shopcart[i].PurchaseStatus.Name;
                lit.ShopId = shopcart[i].ShopId;
                var shopid = shopcart[i].ShopId;
                var cartlist = db.Repository<ShopCartList>().Get(p => p.ShopId == shopid).ToList();
                itemmodel = new List<HistoryItemModel>();
                for (var j = 0; j < cartlist.Count; j++)
                {
                    HistoryItemModel lim = new HistoryItemModel();
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
        public SectionDetailModel GetProductDetail(string ProductId)
        {
            #region 一般商品資訊
            SectionDetailModel model = new SectionDetailModel();
            var defaultmodel = db.Repository<Product>().Get(p => p.ProductId == ProductId).FirstOrDefault();
            model.ProductId = defaultmodel.ProductId;
            model.ProductMemo = defaultmodel.ProductMemo1;
            model.ProductName = defaultmodel.ProductName;
            model.ProductPrice = defaultmodel.ProductPrice;
            model.ProductSpec = defaultmodel.ProductSpec;
            model.ProductImgUrl1 = defaultmodel.ProductImgUrl1;
            model.ProductImgUrl2 = defaultmodel.ProductImgUrl2;
            model.ProductImgUrl3 = defaultmodel.ProductImgUrl3;
            model.ProductImgUrl4 = defaultmodel.ProductImgUrl4;
            model.OrgPrice = defaultmodel.OrgPrice;
            var sharp = db.Repository<ProductColor>().Get(p => p.ProductId == ProductId).Select(p => p.Color1.Sharp).ToList();
            model.Sharp = new List<string>();
            model.Sharp = sharp;
            model.Time = DateTime.Parse(defaultmodel.Time.ToString()).ToString("yyyy/MM/dd");
            model.ProductImgUrl1 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ProductImgUrl1;
            if (model.ProductImgUrl2 != null)
            {
                model.ProductImgUrl2 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ProductImgUrl2;
            }
            if (model.ProductImgUrl3 != null)
            {
                model.ProductImgUrl3 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ProductImgUrl3;
            }
            if (model.ProductImgUrl4 != null)
            {
                model.ProductImgUrl4 = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + model.ProductImgUrl4;
            }
            var allstar = db.Repository<ProductRating>().Get(p => p.ProductId == ProductId).Select(p => p.Star).ToList();
            double totalstar = 0;
            for (var i = 0; i < allstar.Count; i++)
            {
                totalstar += allstar[i];
            }
            totalstar = totalstar / (allstar.Count);
            model.ProductCategory = db.Repository<CategoryTable>().Get(p => p.ProductId == ProductId).ToList();
            model.Star = Math.Round(totalstar, 1);
            #endregion
            #region 推薦(同類)
            var categoryid = db.Repository<CategoryTable>().Get(p => p.ProductId == ProductId).Select(p => p.Id).FirstOrDefault();
            var allitem = db.Repository<CategoryTable>().Get(p => p.Id == categoryid && p.ProductId != ProductId).Select(p => new CategoryItem()
            {
                ProductId = p.ProductId,
                ProductImgUrl = p.Product.ProductImgUrl1,
                ProductName = p.Product.ProductName,
                ProductPrice = p.Product.ProductPrice
            }).ToList();
            List<CountProduct> productquantity = new List<CountProduct>();//計算用
            List<SectionDetailItem> categoryitem = new List<SectionDetailItem>();
            #region 取出銷售總額

            List<GetHotModel> hotmodel = new List<GetHotModel>();//存放ProductId+Quantity
            for (var j = 0; j < allitem.Count; j++)//加總所有已結帳數量
            {
                string getproductid = allitem[j].ProductId;
                var getyeartotal = db.Repository<ShopCartList>()
                    .Get(p => p.ShopCart.Status == true && p.ProductId == getproductid)
                    .Select(p => p.Quantity).ToList();
                int quantity = 0;
                for (var k = 0; k < getyeartotal.Count; k++)//加總數量
                {
                    quantity += Int32.Parse(getyeartotal[k].ToString());
                }
                hotmodel.Add(new GetHotModel() { ProductName = allitem[j].ProductId, Quantity = quantity });//借用
            }
            hotmodel = hotmodel.OrderByDescending(p => p.Quantity).Take(4).ToList();
            for (var j = 0; j < hotmodel.Count; j++)//撈詳細資料
            {
                var hotid = hotmodel[j].ProductName;
                categoryitem.Add(db.Repository<Product>().Get(p => p.ProductId == hotid).Select(p => new SectionDetailItem()
                {
                    ProductId = p.ProductId,
                    ProductImgUrl = p.ProductImgUrl1,
                    ProductPrice = p.ProductPrice,
                    ProductName = p.ProductName
                }).FirstOrDefault());
                categoryitem[j].ProductImgUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + categoryitem[j].ProductImgUrl;
                if (categoryitem[j].ProductName.Length >= 8)
                {
                    categoryitem[j].ProductName = categoryitem[j].ProductName.Substring(0, 8) + "...";
                }

                var allstaritem = db.Repository<ProductRating>().Get(p => p.ProductId == hotid).Select(p => p.Star).ToList();
                double totalstaritem = 0;
                for (var u = 0; u < allstaritem.Count; u++)
                {
                    totalstaritem += allstaritem[u];
                }
                totalstaritem = totalstaritem / (allstaritem.Count);
                categoryitem[j].Star = totalstaritem;
            }
            model.categoryitem = new List<SectionDetailItem>();
            model.categoryitem = categoryitem;

            #endregion
            #endregion
            #region 推薦(關聯)
            var getalllist = db.Repository<ShopCartList>().Get(p => p.ProductId == ProductId && p.ShopCart.Status == true).Select(p => p.ShopId).ToList();//取得所有符合PID的ShopId
            List<CountProduct> countproduct = new List<CountProduct>();
            for (var i = 0; i < getalllist.Count; i++)
            {
                var shopid = getalllist[i];
                var getsinglelist = db.Repository<ShopCartList>().Get(p => p.ShopId == shopid && p.ProductId != ProductId).Select(p => p.ProductId).ToList();//取出所有ShopId符合的PID
                var isthere = false;
                for (var w = 0; w < getsinglelist.Count; w++)//所有PID迴圈
                {
                    for (var j = 0; j < countproduct.Count; j++)//所有存放陣列PID
                    {
                        if (getsinglelist[w] == countproduct[j].ProductId) //已存在改數量
                        {
                            countproduct[j].Quantity += 1;
                            isthere = true;
                            break;
                        }
                    }
                    if (!isthere)
                    {
                        countproduct.Add(new CountProduct() { ProductId = getsinglelist[w], Quantity = 1 });
                    }
                }
            }
            countproduct=countproduct.OrderByDescending(p => p.Quantity).Take(4).ToList();
            List<SectionDetailItem> detailitem = new List<SectionDetailItem>();
            for (var i = 0; i < countproduct.Count; i++)
            {
                var pid = countproduct[i].ProductId;
                detailitem.Add(db.Repository<Product>().Get(p => p.ProductId == pid).Select(p => new SectionDetailItem()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    ProductImgUrl = p.ProductImgUrl1
                }).FirstOrDefault());
                var allstaritem = db.Repository<ProductRating>().Get(p => p.ProductId == pid).Select(p => p.Star).ToList();
                double totalstaritem = 0;
                for (var u = 0; u < allstaritem.Count; u++)
                {
                    totalstaritem += allstaritem[u];
                }
                totalstaritem = totalstaritem / (allstaritem.Count);
                if (detailitem[i].ProductName.Length > 8)
                {
                    detailitem[i].ProductName = detailitem[i].ProductName.Substring(0, 8) + "...";
                }
                detailitem[i].Star = totalstaritem;
                detailitem[i].ProductImgUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + detailitem[i].ProductImgUrl;
            }
            model.detailitem = detailitem;
            #endregion
            #region 推薦(分群)

            if (HttpContext.Current.Request.Cookies["UIC"]!=null)
            {
                var User = HttpContext.Current.Request.Cookies["UIC"]["Name"];
                var userinfo = db.Repository<Member>().Get(p => p.Account == User).FirstOrDefault();
                var userjob = userinfo.Job;
                var usersex = userinfo.Sex;
                var userstyle = userinfo.Style;
                var usermoney = userinfo.Money;
                model.UserStatus = "與您一樣是 ";
                model.UserStatus += (usersex == true) ? "男性" : "女性";
                model.UserStatus += ",";
                model.UserStatus += (userjob == 0) ? "國小/國中生" : (userjob == 1) ? "高中生/五專生" : (userjob == 2) ? "大學生" : (userjob == 3) ? "上班族" : "自營業";
                model.UserStatus += ",月收入:";
                model.UserStatus += (usermoney == 0) ? "無收入" : (usermoney == 1) ? "20000以下" : (usermoney == 2) ? "20000~50000" : "50000以上";
                model.UserStatus += ",喜歡";
                model.UserStatus += (userstyle == 1) ? "美式風格" : (userstyle == 2) ? "歐式風格" : (userstyle == 3) ? "法式風格" : (userstyle == 4) ? "鄉村風格" : "中式風格";
                model.UserStatus += "的使用者也喜歡:";
                var matchguy = db.Repository<Member>().Get(p => p.Job == userjob && p.Sex == usersex && p.Style == userstyle && p.Money == usermoney && p.Account != User).Select(p => p.Account).ToList();
                List<string> matchcartid = new List<string>();
                for (var i = 0; i < matchguy.Count; i++)//取出所有符合者的消費單據
                {
                    var userid = matchguy[i];
                    var usercart = db.Repository<ShopCart>().Get(p => p.Account == userid && p.Status == true).Select(p => p.ShopId).ToList();
                    for (var j = 0; j < usercart.Count; j++)
                    {
                        matchcartid.Add(usercart[j]);
                    }
                }
                countproduct = new List<CountProduct>();
                for (var i = 0; i < matchcartid.Count; i++)
                {
                    var shopid = matchcartid[i];
                    var getsinglelist = db.Repository<ShopCartList>().Get(p => p.ShopId == shopid && p.ProductId != ProductId).Select(p => p.ProductId).ToList();//取出所有ShopId符合的PID
                    var isthere = false;
                    for (var w = 0; w < getsinglelist.Count; w++)//所有PID迴圈
                    {
                        for (var j = 0; j < countproduct.Count; j++)//所有存放陣列PID
                        {
                            if (getsinglelist[w] == countproduct[j].ProductId) //已存在改數量
                            {
                                countproduct[j].Quantity += 1;
                                isthere = true;
                                break;
                            }
                        }
                        if (!isthere)
                        {
                            countproduct.Add(new CountProduct() { ProductId = getsinglelist[w], Quantity = 1 });
                        }
                    }
                }
                countproduct=countproduct.OrderByDescending(p => p.Quantity).Take(4).ToList();
                List<SectionDetailItem> matchitem = new List<SectionDetailItem>();
                for (var i = 0; i < countproduct.Count; i++)
                {
                    var pid = countproduct[i].ProductId;
                    matchitem.Add(db.Repository<Product>().Get(p => p.ProductId == pid).Select(p => new SectionDetailItem()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        ProductPrice = p.ProductPrice,
                        ProductImgUrl = p.ProductImgUrl1
                    }).FirstOrDefault());
                    var allstaritem = db.Repository<ProductRating>().Get(p => p.ProductId == pid).Select(p => p.Star).ToList();
                    double totalstaritem = 0;
                    for (var u = 0; u < allstaritem.Count; u++)
                    {
                        totalstaritem += allstaritem[u];
                    }
                    totalstaritem = totalstaritem / (allstaritem.Count);
                    matchitem[i].Star = totalstaritem;
                    if (matchitem[i].ProductName.Length > 8)
                    {
                        matchitem[i].ProductName = matchitem[i].ProductName.Substring(0, 8) + "...";
                    }
                    matchitem[i].ProductImgUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + matchitem[i].ProductImgUrl;
                }
                model.matchitem = matchitem;
            }
            #endregion
            #region 問答讀取
            var allreply = db.Repository<Reply>().Get(p => p.ProductId == ProductId && p.RId == null).OrderBy(p => p.Time).ThenBy(p => p.Id).ToList();//先撈問
            List<ReplyModel> replylist = new List<ReplyModel>();
            for (var i = 0; i < allreply.Count; i++)
            {
                var rid = allreply[i].Id;
                var getid = allreply[i].Account;
                var getname = db.Repository<Member>().Get(p => p.Account == getid).FirstOrDefault();
                replylist.Add(new ReplyModel() { Account = getid, Name = getname.Name, Id = allreply[i].Id, ProductId = allreply[i].ProductId, RId = allreply[i].RId, Text = allreply[i].Text, Time = allreply[i].Time.ToString("yyyy-MM-dd") });
                var thereply = db.Repository<Reply>().Get(p => p.ProductId == ProductId && p.RId == rid).FirstOrDefault();
                if (thereply != null)
                {
                    getid = thereply.Account;
                    getname = db.Repository<Member>().Get(p => p.Account == getid).FirstOrDefault();
                    replylist.Add(new ReplyModel() { Account = getid, Name = getname.Name, Id = thereply.Id, ProductId = thereply.ProductId, RId = thereply.RId, Text = thereply.Text, Time = thereply.Time.ToString("yyyy-MM-dd") });
                }
            }
            model.replyitem = new List<ReplyModel>();
            model.replyitem = replylist;
            #endregion
            return model;
        }
        public IndexProductModel GetIndex()
        {
            IndexProductModel model = new IndexProductModel();
            #region 首頁本月推薦(手動輸入)
            model.PushHotItem = new List<SectionDetailItem>();
            var detailitem = db.Repository<Product>().Get(p => p.PushHot == true).Select(p => new SectionDetailItem()
            {
                ProductId = p.ProductId,
                ProductImgUrl = p.ProductImgUrl1,
                ProductPrice = p.ProductPrice,
                ProductName = p.ProductName
            }).Take(6).ToList();
            for (var i = 0; i < detailitem.Count; i++)
            {
                var pid = detailitem[i].ProductId;
                var allstaritem = db.Repository<ProductRating>().Get(p => p.ProductId == pid).Select(p => p.Star).ToList();
                double totalstaritem = 0;
                for (var u = 0; u < allstaritem.Count; u++)
                {
                    totalstaritem += allstaritem[u];
                }
                totalstaritem = totalstaritem / (allstaritem.Count);
                detailitem[i].Star = totalstaritem;
                detailitem[i].ProductImgUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + detailitem[i].ProductImgUrl;
                if (detailitem[i].ProductName.Length > 9)
                {
                    detailitem[i].ProductName = detailitem[i].ProductName.Substring(0, 9) + "...";
                }
            }
            model.PushHotItem = detailitem;
            #endregion
            #region 特賣(系統判定銷售量+橫幅顯示)

            List<ProductCategoryModel> categorymodel = new List<ProductCategoryModel>();
            categorymodel = db.Repository<ProductCategory>().Get().Select(p => new ProductCategoryModel()//取得所有類別
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Summary = p.Summary
            }).ToList();
            for (var i = 0; i < categorymodel.Count; i++)//取得所有同類別的商品
            {
                int categoryid = categorymodel[i].Id;
                categorymodel[i].Name += "特輯";
                categorymodel[i].ImageUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + categorymodel[i].ImageUrl;
                var allitem = db.Repository<CategoryTable>().Get(p => p.Id == categoryid).Select(p => new CategoryItem()
                {
                    ProductId = p.ProductId,
                    ProductImgUrl = p.Product.ProductImgUrl1,
                    ProductName = p.Product.ProductName,
                    ProductPrice = p.Product.ProductPrice,
                    OrgPrice=p.Product.OrgPrice
                }).ToList();
                List<CountProduct> productquantity = new List<CountProduct>();//計算用
                List<CategoryItem> categoryitem = new List<CategoryItem>();
                List<CategoryItem> onsaleitem = new List<CategoryItem>();
                #region 取出銷售總額

                List<GetHotModel> hotmodel = new List<GetHotModel>();//存放ProductId+Quantity
                for (var j = 0; j < allitem.Count; j++)//加總所有已結帳數量
                {
                    if (allitem[j].OrgPrice != null)
                    {
                        onsaleitem.Add(new CategoryItem() { ProductId = allitem[j].ProductId, ProductImgUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath+allitem[j].ProductImgUrl, OrgPrice = allitem[j].OrgPrice, ProductPrice = allitem[j].ProductPrice, ProductName = allitem[j].ProductName });
                    }
                    string getproductid = allitem[j].ProductId;
                    var getyeartotal = db.Repository<ShopCartList>()
                        .Get(p => p.ShopCart.Status == true && p.ProductId == getproductid)
                        .Select(p => p.Quantity).ToList();
                    int quantity = 0;
                    for (var k = 0; k < getyeartotal.Count; k++)//加總數量
                    {
                        quantity += Int32.Parse(getyeartotal[k].ToString());
                    }
                    hotmodel.Add(new GetHotModel() { ProductName = allitem[j].ProductId, Quantity = quantity });//借用
                }
                hotmodel = hotmodel.OrderByDescending(p => p.Quantity).Take(4).ToList();
                for (var j = 0; j < hotmodel.Count; j++)//撈詳細資料
                {
                    var hotid = hotmodel[j].ProductName;
                    categoryitem.Add(db.Repository<Product>().Get(p => p.ProductId == hotid).Select(p => new CategoryItem()
                    {
                        ProductId = p.ProductId,
                        ProductImgUrl = p.ProductImgUrl1,
                        ProductPrice = p.ProductPrice,
                        ProductName = p.ProductName,
                        ProductMemo = p.ProductMemo1
                    }).FirstOrDefault());
                    categoryitem[j].ProductImgUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + categoryitem[j].ProductImgUrl;
                    if (categoryitem[j].ProductName.Length >= 8)
                    {
                        categoryitem[j].ProductName = categoryitem[j].ProductName.Substring(0, 8) + "...";
                    }
                    categoryitem[j].ProductMemo = categoryitem[j].ProductMemo.Replace("<br>", "");
                    if (categoryitem[j].ProductMemo.Length > 20)
                    {
                        categoryitem[j].ProductMemo = categoryitem[j].ProductMemo.Substring(0, 20) + "...";
                    }
                    var allstaritem = db.Repository<ProductRating>().Get(p => p.ProductId == hotid).Select(p => p.Star).ToList();
                    double totalstaritem = 0;
                    for (var u = 0; u < allstaritem.Count; u++)
                    {
                        totalstaritem += allstaritem[u];
                    }
                    totalstaritem = totalstaritem / (allstaritem.Count);
                    categoryitem[j].Star = totalstaritem;
                }
                categorymodel[i].item = new List<CategoryItem>();
                categorymodel[i].item = categoryitem;
                categorymodel[i].onsaleitem = new List<CategoryItem>();
                categorymodel[i].onsaleitem = onsaleitem;
                model.Special = new List<ProductCategoryModel>();
                model.Special = categorymodel;
            }
                #endregion
            #endregion
            #region 獲取最新消息

            var allnews = db.Repository<News>().Get().OrderByDescending(p => p.Time).Take(20).ToList();
            List<ShowNews> getnews = new List<ShowNews>();
            for (var i = 0; i < allnews.Count; i++)
            {
                getnews.Add(new ShowNews() { Id = allnews[i].Id, Title = (allnews[i].Title.Length > 38) ? allnews[i].Title.Substring(0, 38) + "..." : allnews[i].Title, Time = allnews[i].Time.ToString("yyyy.MM.dd") });
            }
            model.CountNews = getnews.Count();
            model.ShowNews = new List<ShowNews>();
            model.ShowNews = getnews;
            #endregion
            return model;
        }
        #region 評價星數
        public bool RateStar(double star, string ProductId)
        {
            try
            {
                var Account = HttpContext.Current.Request.Cookies["UIC"]["Name"];
                ProductRating checkoldrate = new ProductRating();
                if (db.Repository<ProductRating>().Get(p => p.ProductId == ProductId && p.Account == Account).Any())//更新評價
                {
                    checkoldrate.Account = Account;
                    checkoldrate.Star = star;
                    checkoldrate.ProductId = ProductId;
                    db.Repository<ProductRating>().UpdateAll(checkoldrate);
                }
                else//新增評價
                {
                    checkoldrate.Account = Account;
                    checkoldrate.Star = star;
                    checkoldrate.ProductId = ProductId;
                    db.Repository<ProductRating>().Create(checkoldrate);
                }
                db.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
        public void ReplyTo(int Id, string ProductId, string replyContext)
        {
            Reply model = new Reply();
            string User = HttpContext.Current.Request.Cookies["UIC"]["Name"];
            model.Account = User;
            model.ProductId = ProductId;
            model.Text = replyContext.Replace("\n", "<br>");
            model.Time = DateTime.Now;
            model.RId = Id;
            db.Repository<Reply>().Create(model);
            db.Save();
        }
        public void Reply(string ProductId, string replyContext)
        {
            Reply model = new Reply();
            string User = HttpContext.Current.Request.Cookies["UIC"]["Name"];
            model.Account = User;
            model.ProductId = ProductId;
            model.Text = replyContext.Replace("\n", "<br>");
            model.Time = DateTime.Now;
            db.Repository<Reply>().Create(model);
            db.Save();
        }
        public NewsPageModel GetNews()
        {
            NewsPageModel model = new NewsPageModel();
            var news = db.Repository<News>().Get().ToList();
            model.ShowNews = new List<ShowNews>();
            for (var i = 0; i < news.Count; i++)
            {
                model.ShowNews.Add(new ShowNews()
                {
                    Id = news[i].Id,
                    Time = news[i].Time.ToString("yyyy.MM.dd"),
                    Title = (news[i].Title.Length > 37) ? news[i].Title.Substring(0, 37) + "..." : news[i].Title
                });
            }
            model.CountNews = news.Count;
            return model;
        }

        public NewsDetailModel GetNewsDetail(int Id)
        {
            var news = db.Repository<News>().Get(p => p.Id == Id).FirstOrDefault();
            NewsDetailModel model = new NewsDetailModel();
            model.Id = news.Id;
            model.ImageUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + news.ImageUrl;
            model.Time = news.Time.ToString("yyyy/MM/dd");
            model.Title = news.Title;
            model.Text = news.Text;
            return model;
        }
        public string GetApk()
        {
            return db.Repository<ApkUrl>().Get(p => p.IsServer == true).Select(p => p.ApkUrl1).FirstOrDefault();
        }
    }
}