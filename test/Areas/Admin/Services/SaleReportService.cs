using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services.Generic;

namespace test.Areas.Admin.Services
{
    public class SaleReportService : DetailViewService
    {
        DBEntity db = new DBEntity();
        public SaleReportModel GetViewData()
        {
            SaleReportModel model = new SaleReportModel(true);
            model.Select1 = 0;
            return model;
        }
        public DetailSearchDataModel<SaleReportGridModel> Search(SaleReportModel jsonData)
        {
            string[] Selector = { "shopid" };

            var SearchResult = DetailService.GetDetailSub<SaleReportGridModel>
                (Selector, jsonData, new string[] { "shopid" }, tableName: "FuckSaleReport");

            int page = SearchResult.Item1;

            System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext);
            List<SaleReportGridModel> aaData = SearchResult.Item2;

            return new DetailSearchDataModel<SaleReportGridModel> { draw = jsonData.Draw, recordsTotal = page, recordsFiltered = page, data = aaData };
        }
        /// <summary>
        /// 月損益比較
        /// </summary>
        /// <param name="total"></param>
        /// <param name="income"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        public List<string[]> GetMonthReport(bool total, bool income, bool final)
        {
            List<string[]> result = new List<string[]>();

            #region 取出所有銷售資料
            SaleReportModel model = new SaleReportModel(true);
            model.Sort = "asc";
            model.SortData = "shopid";
            model.Start = 0;
            model.Length = 75;
            model.Draw = 1;
            string[] Selector = { "shopid" };
            string[] title = { };
            byte count = 0;
            if (total)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "營收";
                count += 1;
            }
            if (income)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "毛利";
                count += 1;
            }
            if (final)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "利益";
                count += 1;
            }

            result.Add(title);
            var SearchResult = DetailService.GetDetailSub<SaleReportGridModel>
                (Selector, model, new string[] { "shopid" }, tableName: "FuckSaleReport");
            List<SaleReportGridModel> aaData = SearchResult.Item2;
            #endregion

            #region 加入前年到目前為止12個月的資料
            DateTime dt = DateTime.Now;
            bool pushhead = true;

            #region 營收
            if (total)
            {
                int year = dt.Year - 1;
                int month = dt.Month;
                string[] monthdata = new string[13];
                string[] monthtitle = new string[13];
                for (var i = 0; i < 13; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthdata[i] = item.total;
                            break;
                        }
                    }
                    if (monthdata[i] == null)
                    {
                        monthdata[i] = "0";
                    }
                    if (pushhead)
                    {
                        monthtitle[i] = year.ToString() + "年" + String.Format("{0:00}", month) + "月";
                    }
                    if (month + 1 <= 12)
                    {

                        month += 1;
                    }
                    else
                    {
                        year += 1;
                        month = 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                result.Add(monthdata);
            }
            #endregion
            #region 毛利
            if (income)
            {
                int year = dt.Year - 1;
                int month = dt.Month;
                string[] monthdata = new string[13];
                string[] monthtitle = new string[13];
                for (var i = 0; i < 13; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthdata[i] = item.income;
                            break;
                        }
                    }
                    if (monthdata[i] == null)
                    {
                        monthdata[i] = "0";
                    }
                    if (pushhead)
                    {
                        monthtitle[i] = year.ToString() + "年" + String.Format("{0:00}", month) + "月";
                    }
                    if (month + 1 <= 12)
                    {

                        month += 1;
                    }
                    else
                    {
                        year += 1;
                        month = 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                result.Add(monthdata);
            }
            #endregion
            #region 利益
            if (final)
            {
                int year = dt.Year - 1;
                int month = dt.Month;
                string[] monthdata = new string[13];
                string[] monthtitle = new string[13];
                for (var i = 0; i < 13; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthdata[i] = item.final;
                            break;
                        }
                    }
                    if (monthdata[i] == null)
                    {
                        monthdata[i] = "0";
                    }
                    if (pushhead)
                    {
                        monthtitle[i] = year.ToString() + "年" + String.Format("{0:00}", month) + "月";
                    }
                    if (month + 1 <= 12)
                    {

                        month += 1;
                    }
                    else
                    {
                        year += 1;
                        month = 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                result.Add(monthdata);
            }
            #endregion
            #endregion

            return result;
        }
        /// <summary>
        /// 季損益比較
        /// </summary>
        /// <param name="total"></param>
        /// <param name="income"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        public List<string[]> GetSeasonReport(bool total, bool income, bool final)
        {
            List<string[]> result = new List<string[]>();

            #region 取出所有銷售資料
            SaleReportModel model = new SaleReportModel(true);
            model.Sort = "asc";
            model.SortData = "shopid";
            model.Start = 0;
            model.Length = 75;
            model.Draw = 1;
            string[] Selector = { "shopid" };
            string[] title = { };
            byte count = 0;
            if (total)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "營收";
                count += 1;
            }
            if (income)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "毛利";
                count += 1;
            }
            if (final)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "利益";
                count += 1;
            }

            result.Add(title);
            var SearchResult = DetailService.GetDetailSub<SaleReportGridModel>
                (Selector, model, new string[] { "shopid" }, tableName: "FuckSaleReport");
            List<SaleReportGridModel> aaData = SearchResult.Item2;
            #endregion

            #region 加入前年到目前為止的資料
            DateTime dt = DateTime.Now;
            bool pushhead = true;

            #region 營收
            if (total)
            {
                int year = dt.Year - 1;
                int month = (dt.Month <= 3) ? 4 : (dt.Month <= 6) ? 7 : (dt.Month <= 9) ? 10 : 1;
                int season = 0;
                int[] monthtempdata = new int[4];
                string[] monthdata = new string[4];
                string[] monthtitle = new string[4];
                for (var i = 0; i < 12; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthtempdata[season] += int.Parse(item.total);

                            break;
                        }
                    }
                    if (month == 3 || month == 6 || month == 9 || month == 12)
                    {
                        if (pushhead)
                        {
                            if (month == 3)
                            {
                                monthtitle[season] = year.ToString() + "年春季";
                            }
                            else if (month == 6)
                            {
                                monthtitle[season] = year.ToString() + "年夏季";
                            }
                            else if (month == 9)
                            {
                                monthtitle[season] = year.ToString() + "年秋季";
                            }
                            else if (month == 12)
                            {
                                monthtitle[season] = year.ToString() + "年冬季";

                            }
                        }
                        season += 1;
                    }
                    if (month + 1 > 12)
                    {
                        month = 1;
                        year += 1;
                    }
                    else
                    {
                        month += 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                for (var i = 0; i < 4; i++)
                {
                    monthdata[i] = monthtempdata[i].ToString();
                }
                result.Add(monthdata);
            }
            #endregion
            #region 毛利
            if (income)
            {
                int year = dt.Year - 1;
                int month = (dt.Month <= 3) ? 4 : (dt.Month <= 6) ? 7 : (dt.Month <= 9) ? 10 : 1;
                int season = 0;
                int[] monthtempdata = new int[4];
                string[] monthdata = new string[4];
                string[] monthtitle = new string[4];
                for (var i = 0; i < 12; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthtempdata[season] += int.Parse(item.income);

                            break;
                        }
                    }
                    if (month == 3 || month == 6 || month == 9 || month == 12)
                    {
                        if (pushhead)
                        {
                            if (month == 3)
                            {
                                monthtitle[season] = year.ToString() + "年春季";
                            }
                            else if (month == 6)
                            {
                                monthtitle[season] = year.ToString() + "年夏季";
                            }
                            else if (month == 9)
                            {
                                monthtitle[season] = year.ToString() + "年秋季";
                            }
                            else if (month == 12)
                            {
                                monthtitle[season] = year.ToString() + "年冬季";

                            }
                        }
                        season += 1;
                    }
                    if (month + 1 > 12)
                    {
                        month = 1;
                        year += 1;
                    }
                    else
                    {
                        month += 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                for (var i = 0; i < 4; i++)
                {
                    monthdata[i] = monthtempdata[i].ToString();
                }
                result.Add(monthdata);
            }
            #endregion
            #region 利益
            if (final)
            {
                int year = dt.Year - 1;
                int month = (dt.Month <= 3) ? 4 : (dt.Month <= 6) ? 7 : (dt.Month <= 9) ? 10 : 1;
                int season = 0;
                int[] monthtempdata = new int[4];
                string[] monthdata = new string[4];
                string[] monthtitle = new string[4];
                for (var i = 0; i < 12; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthtempdata[season] += int.Parse(item.final);

                            break;
                        }
                    }
                    if (month == 3 || month == 6 || month == 9 || month == 12)
                    {
                        if (pushhead)
                        {
                            if (month == 3)
                            {
                                monthtitle[season] = year.ToString() + "年春季";
                            }
                            else if (month == 6)
                            {
                                monthtitle[season] = year.ToString() + "年夏季";
                            }
                            else if (month == 9)
                            {
                                monthtitle[season] = year.ToString() + "年秋季";
                            }
                            else if (month == 12)
                            {
                                monthtitle[season] = year.ToString() + "年冬季";

                            }
                        }
                        season += 1;
                    }
                    if (month + 1 > 12)
                    {
                        month = 1;
                        year += 1;
                    }
                    else
                    {
                        month += 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                for (var i = 0; i < 4; i++)
                {
                    monthdata[i] = monthtempdata[i].ToString();
                }
                result.Add(monthdata);
            }
            #endregion

            #endregion

            return result;
        }
        /// <summary>
        /// 年損益比較
        /// </summary>
        /// <param name="total"></param>
        /// <param name="income"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        public List<string[]> GetYearReport(bool total, bool income, bool final)
        {
            List<string[]> result = new List<string[]>();

            #region 取出所有銷售資料
            SaleReportModel model = new SaleReportModel(true);
            model.Sort = "asc";
            model.SortData = "shopid";
            model.Start = 0;
            model.Length = 75;
            model.Draw = 1;
            string[] Selector = { "shopid" };
            string[] title = { };
            byte count = 0;
            if (total)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "營收";
                count += 1;
            }
            if (income)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "毛利";
                count += 1;
            }
            if (final)
            {
                System.Array.Resize(ref title, title.Length + 1);
                title[count] = "利益";
                count += 1;
            }

            result.Add(title);
            var SearchResult = DetailService.GetDetailSub<SaleReportGridModel>
                (Selector, model, new string[] { "shopid" }, tableName: "FuckSaleReport");
            List<SaleReportGridModel> aaData = SearchResult.Item2;
            #endregion

            #region 加入前年到目前為止的資料
            DateTime dt = DateTime.Now;
            bool pushhead = true;

            #region 營收
            if (total)
            {
                int year = dt.Year - 2;
                int month = 1;
                int countyear = 0;
                int[] monthtempdata = new int[3];
                string[] monthdata = new string[3];
                string[] monthtitle = new string[3];
                for (var i = 0; i < 36; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthtempdata[countyear] += int.Parse(item.total);

                            break;
                        }
                    }
                    if (month == 12)
                    {
                        if (pushhead)
                        {
                            monthtitle[countyear] = year.ToString() + "年";
                        }
                        countyear += 1;
                    }
                    if (month + 1 > 12)
                    {
                        month = 1;
                        year += 1;
                    }
                    else
                    {
                        month += 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                for (var i = 0; i < 3; i++)
                {
                    monthdata[i] = monthtempdata[i].ToString();
                }
                result.Add(monthdata);
            }
            #endregion
            #region 毛利
            if (income)
            {
                int year = dt.Year - 2;
                int month = 1;
                int countyear = 0;
                int[] monthtempdata = new int[3];
                string[] monthdata = new string[3];
                string[] monthtitle = new string[3];
                for (var i = 0; i < 36; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthtempdata[countyear] += int.Parse(item.income);

                            break;
                        }
                    }
                    if (month == 12)
                    {
                        if (pushhead)
                        {
                            monthtitle[countyear] = year.ToString() + "年";
                        }
                        countyear += 1;
                    }
                    if (month + 1 > 12)
                    {
                        month = 1;
                        year += 1;
                    }
                    else
                    {
                        month += 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                for (var i = 0; i < 3; i++)
                {
                    monthdata[i] = monthtempdata[i].ToString();
                }
                result.Add(monthdata);
            }
            #endregion
            #region 利益
            if (final)
            {
                int year = dt.Year - 2;
                int month = 1;
                int countyear = 0;
                int[] monthtempdata = new int[3];
                string[] monthdata = new string[3];
                string[] monthtitle = new string[3];
                for (var i = 0; i < 36; i++)
                {
                    foreach (SaleReportGridModel item in aaData)
                    {
                        string date = year + String.Format("{0:00}", month);
                        if (item.shopid == date)
                        {
                            monthtempdata[countyear] += int.Parse(item.final);

                            break;
                        }
                    }
                    if (month == 12)
                    {
                        if (pushhead)
                        {
                            monthtitle[countyear] = year.ToString() + "年";
                        }
                        countyear += 1;
                    }
                    if (month + 1 > 12)
                    {
                        month = 1;
                        year += 1;
                    }
                    else
                    {
                        month += 1;
                    }
                }
                if (pushhead)
                {
                    result.Add(monthtitle);
                    pushhead = false;
                }
                for (var i = 0; i < 3; i++)
                {
                    monthdata[i] = monthtempdata[i].ToString();
                }
                result.Add(monthdata);
            }
            #endregion

            #endregion

            return result;
        }
    }
}