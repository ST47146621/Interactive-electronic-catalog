using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using test.Models;

namespace test.Services
{
    public class ChartsService
    {
        DBEntity db = new DBEntity();
        /// <summary>
        /// 周銷售量資料取得
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetWeekTotal(string[] productlist)
        {
            if (productlist.Length != 0)
            {
                #region 取出產品列表
                List<string[]> model = new List<string[]>();
                //var getname = db.Repository<Product>().Get().Select(p => new GetProductNameModel()
                //{
                //    ProductId = p.ProductId,
                //    ProductName = p.ProductName
                //}).ToList();
                List<GetProductNameModel> getname = new List<GetProductNameModel>();
                foreach (var listid in productlist)
                {
                    getname.Add(db.Repository<Product>().Get(p => p.ProductId == listid).Select(p => new GetProductNameModel()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName
                    }).FirstOrDefault());
                }
                string[] result = { getname[0].ProductName };

                for (var i = 1; i < getname.Count; i++)
                {
                    System.Array.Resize(ref result, result.Length + 1);
                    result[i] = getname[i].ProductName;
                }
                model.Add(result);
                #endregion

                #region 取出銷售總額

                DateTime dt = DateTime.Now; //現在時間
                dt = dt.AddDays(1 - dt.Day); //本月月初
                DateTime endMonth = dt.AddMonths(1).AddDays(-1); //本月月底
                bool addmonth = true;
                DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))); //月初的周一
                DateTime endWeek = startWeek.AddDays(6); //月初的周日
                int countweek = 1;
                while (addmonth)
                {
                    if (startWeek.AddDays(7) <= endMonth)
                    {

                    }
                    else
                    {
                        TimeSpan ts = (endMonth - startWeek);
                        int iDays = ts.Days;
                        endWeek = startWeek.AddDays(iDays);
                    }
                    result = new string[] { countweek.ToString() };
                    for (var i = 0; i < getname.Count; i++)
                    {
                        string getproductid = getname[i].ProductId;
                        var getweektotal = db.Repository<ShopCartList>()
                            .Get(p => p.ShopCart.Status == true && p.ShopCart.Date >= startWeek && p.ShopCart.Date <= endWeek && p.ProductId == getproductid)
                            .Select(p => p.Quantity).ToList();
                        int quantity = 0;
                        for (var j = 0; j < getweektotal.Count; j++)
                        {
                            quantity += Int32.Parse(getweektotal[j].ToString());
                        }
                        System.Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = quantity.ToString();
                    }
                    model.Add(result);
                    if (startWeek.AddDays(7) > endMonth)
                    {
                        addmonth = false;
                    }
                    else
                    {
                        startWeek = startWeek.AddDays(7);
                        endWeek = startWeek.AddDays(6);
                        countweek += 1;
                    }
                }
                return model;

                #endregion
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 月銷售量資料取得
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetMonthTotal(string[] productlist)
        {
            if (productlist.Length != 0)
            {
                #region 取出產品列表
                List<string[]> model = new List<string[]>();
                //var getname = db.Repository<Product>().Get().Select(p => new GetProductNameModel()
                //{
                //    ProductId = p.ProductId,
                //    ProductName = p.ProductName
                //}).ToList();
                List<GetProductNameModel> getname = new List<GetProductNameModel>();
                foreach (var listid in productlist)
                {
                    getname.Add(db.Repository<Product>().Get(p => p.ProductId == listid).Select(p => new GetProductNameModel()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName
                    }).FirstOrDefault());
                }
                string[] result = { getname[0].ProductName };

                for (var i = 1; i < getname.Count; i++)
                {
                    System.Array.Resize(ref result, result.Length + 1);
                    result[i] = getname[i].ProductName;
                }
                model.Add(result);
                #endregion

                #region 取出銷售總額

                for (var m = 1; m <= 12; m++)
                {
                    DateTime startmonth = DateTime.Parse(DateTime.Now.Year + "-" + m + "-1");
                    DateTime endmonth = startmonth.AddMonths(1);
                    result = new string[] { m.ToString() };
                    for (var i = 0; i < getname.Count; i++)
                    {
                        string getproductid = getname[i].ProductId;
                        var getmonthtotal = db.Repository<ShopCartList>()
                            .Get(p => p.ShopCart.Status == true && p.ShopCart.Date >= startmonth && p.ShopCart.Date <= endmonth && p.ProductId == getproductid)
                            .Select(p => p.Quantity).ToList();
                        int quantity = 0;
                        for (var j = 0; j < getmonthtotal.Count; j++)
                        {
                            quantity += Int32.Parse(getmonthtotal[j].ToString());
                        }
                        System.Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = quantity.ToString();
                    }
                    model.Add(result);
                }
                return model;

                #endregion
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 年銷售量資料取得
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetYearTotal(string[] productlist)
        {
            if (productlist.Length != 0)
            {
                #region 取出產品列表
                List<string[]> model = new List<string[]>();
                //var getname = db.Repository<Product>().Get().Select(p => new GetProductNameModel()
                //{
                //    ProductId = p.ProductId,
                //    ProductName = p.ProductName
                //}).ToList();
                List<GetProductNameModel> getname = new List<GetProductNameModel>();
                foreach (var listid in productlist)
                {
                    getname.Add(db.Repository<Product>().Get(p => p.ProductId == listid).Select(p => new GetProductNameModel()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName
                    }).FirstOrDefault());
                }
                string[] result = { getname[0].ProductName };

                for (var i = 1; i < getname.Count; i++)
                {
                    System.Array.Resize(ref result, result.Length + 1);
                    result[i] = getname[i].ProductName;
                }
                model.Add(result);
                #endregion

                #region 取出銷售總額

                for (var m = (DateTime.Now.Year - 2); m <= DateTime.Now.Year; m++)
                {
                    DateTime startyear = DateTime.Parse(m + "-1-1");
                    DateTime endyear = startyear.AddYears(1);
                    result = new string[] { m.ToString() };
                    for (var i = 0; i < getname.Count; i++)
                    {
                        string getproductid = getname[i].ProductId;
                        var getyeartotal = db.Repository<ShopCartList>()
                            .Get(p => p.ShopCart.Status == true && p.ShopCart.Date >= startyear && p.ShopCart.Date <= endyear && p.ProductId == getproductid)
                            .Select(p => p.Quantity).ToList();
                        int quantity = 0;
                        for (var j = 0; j < getyeartotal.Count; j++)
                        {
                            quantity += Int32.Parse(getyeartotal[j].ToString());
                        }
                        System.Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = quantity.ToString();
                    }
                    model.Add(result);
                }
                return model;

                #endregion
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 年銷售量資料取得
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetHotTotal()
        {
            #region 取出產品列表
            List<string[]> model = new List<string[]>();
            var getname = db.Repository<Product>().Get().Select(p => new GetProductNameModel()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName
            }).ToList();
            string[] result = { };
            #endregion

            #region 取出銷售總額

            List<GetHotModel> hotmodel = new List<GetHotModel>();
            for (var i = 0; i < getname.Count; i++)
            {
                string getproductid = getname[i].ProductId;
                var getyeartotal = db.Repository<ShopCartList>()
                    .Get(p => p.ShopCart.Status == true && p.ProductId == getproductid)
                    .Select(p => p.Quantity).ToList();
                int quantity = 0;
                for (var j = 0; j < getyeartotal.Count; j++)
                {
                    quantity += Int32.Parse(getyeartotal[j].ToString());
                }
                hotmodel.Add(new GetHotModel() { ProductName = getname[i].ProductName, Quantity = quantity });
            }
            for (var i = 0; i < hotmodel.Count - 1; i++)
            {
                for (var j = 0; j < hotmodel.Count - i - 1; j++)
                {
                    if (hotmodel[j].Quantity < hotmodel[j + 1].Quantity)
                    {
                        int temp = hotmodel[j].Quantity;
                        hotmodel[j].Quantity = hotmodel[j + 1].Quantity;
                        hotmodel[j + 1].Quantity = temp;
                    }
                }
            }
            for (var i = 0; i < 3; i++)
            {
                result = new string[] { };
                System.Array.Resize(ref result, result.Length + 1);
                result[result.Length - 1] = hotmodel[i].ProductName;
                System.Array.Resize(ref result, result.Length + 1);
                result[result.Length - 1] = hotmodel[i].Quantity.ToString();
                System.Array.Resize(ref result, result.Length + 1);
                switch (i)
                {
                    case 0:
                        result[result.Length - 1] = "#b87333";
                        break;
                    case 1:
                        result[result.Length - 1] = "silver";
                        break;
                    case 2:
                        result[result.Length - 1] = "gold";
                        break;
                }
                model.Add(result);
            }
            return model;

            #endregion
        }
        /// <summary>
        /// 周銷售量資料取得
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetTestTotal(string[] productlist)
        {
            if (productlist.Length != 0)
            {
                #region 取出產品列表
                List<string[]> model = new List<string[]>();
                List<GetProductNameModel> getname = new List<GetProductNameModel>();
                foreach (var listid in productlist)
                {
                    getname.Add(db.Repository<Product>().Get(p => p.ProductId == listid).Select(p => new GetProductNameModel()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName
                    }).FirstOrDefault());
                }

                string[] result = { getname[0].ProductName };

                for (var i = 1; i < getname.Count; i++)
                {
                    System.Array.Resize(ref result, result.Length + 1);
                    result[i] = getname[i].ProductName;
                }
                model.Add(result);
                #endregion

                #region 取出銷售總額

                DateTime dt = DateTime.Now; //現在時間
                dt = dt.AddDays(1 - dt.Day); //本月月初
                DateTime endMonth = dt.AddMonths(1).AddDays(-1); //本月月底
                bool addmonth = true;
                DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))); //月初的周一
                DateTime endWeek = startWeek.AddDays(6); //月初的周日
                int countweek = 1;
                while (addmonth)
                {
                    if (startWeek.AddDays(7) <= endMonth)
                    {

                    }
                    else
                    {
                        TimeSpan ts = (endMonth - startWeek);
                        int iDays = ts.Days;
                        endWeek = startWeek.AddDays(iDays);
                    }
                    result = new string[] { countweek.ToString() };
                    for (var i = 0; i < getname.Count; i++)
                    {
                        string getproductid = getname[i].ProductId;
                        var getweektotal = db.Repository<ShopCartList>()
                            .Get(p => p.ShopCart.Status == true && p.ShopCart.Date >= startWeek && p.ShopCart.Date <= endWeek && p.ProductId == getproductid)
                            .Select(p => p.Quantity).ToList();
                        int quantity = 0;
                        for (var j = 0; j < getweektotal.Count; j++)
                        {
                            quantity += Int32.Parse(getweektotal[j].ToString());
                        }
                        System.Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = quantity.ToString();
                    }
                    model.Add(result);
                    if (startWeek.AddDays(7) > endMonth)
                    {
                        addmonth = false;
                    }
                    else
                    {
                        startWeek = startWeek.AddDays(7);
                        endWeek = startWeek.AddDays(6);
                        countweek += 1;
                    }
                }
                return model;

                #endregion
            }
            else
            {
                return null;
            }
        }
        public void AddPayMent(string y, string m)
        {
            ProductPayment model = new ProductPayment();
            model.Year = y + "00".Substring(0, 2 - m.Length) + m;
            Random ran = new Random();
            model.Cost = ran.Next(8000, 150000);
            db.Repository<ProductPayment>().Create(model);
            db.Save();
        }
        public void AddWeek(string y, string m, string d)
        {
            ShopCart model = new ShopCart();
            model.Account = "TEST";
            model.ShopId = y + "00".Substring(0, 2 - m.Length) + m + "00".Substring(0, 2 - d.Length) + d + "001";
            model.Status = true;
            model.Date = DateTime.Parse(y + "/" + m + "/" + d);
            model.Payment = 1;
            db.Repository<ShopCart>().Create(model);
            ShopCartList model2 = new ShopCartList();
            var product = db.Repository<Product>().Get().Select(p => p.ProductId).ToList();
            db.Save();
            int a = 1;
            foreach (var item in product)
            {
                //model2.ShopId = model.ShopId;
                //model2.Id = a;
                //model2.ProductId = item;
                //Random ran = new Random();
                //model2.Quantity = ran.Next(10, 70);
                //db.Repository<ShopCartList>().Create(model2);

                //db.Save();
                String query = "INSERT INTO dbo.ShopCartList (ShopId,id,ProductId,Quantity) VALUES (@ShopId,@id, @ProductId, @Quantity)";
                //var connectstring="data source=ERIC-PC\\SQLEXPRESS;initial catalog=ARealShop;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                var connectstring = "data source=163.17.136.196;initial catalog=Fantasy;user id=sa;password=iabf.2014;MultipleActiveResultSets=True;App=EntityFramework";
                using (SqlConnection connection = new SqlConnection(connectstring))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //a shorter syntax to adding parameters
                    command.Parameters.Add("@id", SqlDbType.Int).Value = a;

                    command.Parameters.Add("@ShopId", SqlDbType.NChar).Value = model.ShopId;

                    //a longer syntax for adding parameters
                    command.Parameters.Add("@ProductId", SqlDbType.NChar).Value = item;
                    Random ran = new Random();
                    command.Parameters.Add("@Quantity", SqlDbType.NChar).Value = ran.Next(1, 5);

                    //make sure you open and close(after executing) the connection
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                a += 1;
            }

        }
    }
}