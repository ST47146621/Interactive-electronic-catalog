using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using test.Extend;
using test.Models;

namespace test.Services.Generic
{
    public static class DetailService
    {
        /// <summary>
        /// 取得查詢頁面路徑
        /// </summary>
        /// <param name="name">查詢頁面名稱</param>
        /// <returns></returns>
        public static object GetDetailUrl(string name)
        {
            IUnitOfWork db = new UnitOfWork();

            var detailUrl = db.Repository<DetailURL>().Get(p => p.ViewAttachName.Equals(name))
                .Select(p => new { p.ViewURL, p.Height, p.Width }).FirstOrDefault();

            if (detailUrl == null)
                throw new MyException("找不到寶貝");

            return new
            {
                url = HttpContext.Current.Request.UrlContent(detailUrl.ViewURL),
                width = detailUrl.Width.GetValueOrDefault(),
                height = detailUrl.Height.GetValueOrDefault()
            };
        }

        /// <summary>
        /// 多組條件查詢
        /// </summary>
        /// <typeparam name="TEntity">查詢回傳型態</typeparam>
        /// <param name="selector">查詢欄位</param>
        /// <param name="viewModel">查詢傳入ViewModel</param>
        /// <param name="mainColumn">主Key欄位</param>
        /// <param name="extendSQL">附加查詢SQL</param>
        /// <param name="getColumn">查詢欄位</param>
        /// <returns>資料查詢結果</returns>
        public static Tuple<int, List<TEntity>> GetDetailSub<TEntity>(string[] selector, DetailViewModel viewModel,
            string[] mainColumn = null, string extendSQL = "", string getColumn = "*", string tableName = null)
            where TEntity : class
        {
            IUnitOfWork unitOfWork = new UnitOfWork();

            //初始SQL查詢所需
            if (String.IsNullOrEmpty(tableName))
                tableName = typeof(TEntity).Name;

            StringBuilder SQL = new StringBuilder(String.Format("SELECT %ALLCOLUMN% FROM {0} AS a", tableName));
            Regex regexColumn = new Regex("%ALLCOLUMN%"); //取得欄位用

            //
            string[] Sign = 
            {
                " LIKE @{0} +'%'",
                " LIKE '%' + @{0} +'%'", 
                " > @{0}", " >= @{0}", 
                " < @{0}", " <= @{0}",
                " != @{0}"
            };
            //
            string[] Condition = 
            { 
                " OR ",
                " AND "
            };

            #region 過濾條件

            List<SqlParameter> Parameter = new List<SqlParameter>();
            // 第一組過濾
            if (String.IsNullOrEmpty(viewModel.Filter1) == false)
            {
                SQL.Append(String.Format(" WHERE a.{0} {1}", selector[viewModel.Select1],
                    String.Format(Sign[viewModel.Sign1], selector[viewModel.Select1] + "_1")));
                Parameter.Add(new SqlParameter("@" + selector[viewModel.Select1] + "_1", viewModel.Filter1));
            }

            // 第二組過濾
            if (String.IsNullOrEmpty(viewModel.Filter1) == false && String.IsNullOrEmpty(viewModel.Filter2) == false)
            {
                SQL.Append(String.Format("{2} a.{0} {1}", selector[viewModel.Select2],
                    String.Format(Sign[viewModel.Sign2], selector[viewModel.Select2] + "_2"), Condition[viewModel.Condition1]));
                Parameter.Add(new SqlParameter("@" + selector[viewModel.Select2] + "_2", viewModel.Filter2));
            }
            else if (String.IsNullOrEmpty(viewModel.Filter2) == false)
            {
                SQL.Append(String.Format(" WHERE a.{0} {1}", selector[viewModel.Select2],
                    String.Format(Sign[viewModel.Sign2], selector[viewModel.Select2] + "_2")));
                Parameter.Add(new SqlParameter("@" + selector[viewModel.Select2] + "_2", viewModel.Filter2));
            }

            // 第三組過濾
            if ((String.IsNullOrEmpty(viewModel.Filter2) == false || String.IsNullOrEmpty(viewModel.Filter1) == false) && String.IsNullOrEmpty(viewModel.Filter3) == false)
            {
                SQL.Append(String.Format("{2} a.{0} {1}", selector[viewModel.Select3],
                    String.Format(Sign[viewModel.Sign3], selector[viewModel.Select3] + "_3"), Condition[viewModel.Condition2]));
                Parameter.Add(new SqlParameter("@" + selector[viewModel.Select3] + "_3", viewModel.Filter3));
            }
            else if (String.IsNullOrEmpty(viewModel.Filter3) == false)
            {
                SQL.Append(String.Format(" WHERE a.{0} {1}", selector[viewModel.Select3],
                    String.Format(Sign[viewModel.Sign3], selector[viewModel.Select3] + "_3")));
                Parameter.Add(new SqlParameter("@" + selector[viewModel.Select3] + "_3", viewModel.Filter3));
            }

            //過濾空字串用
            if (mainColumn != null)
            {
                foreach (var column in mainColumn)
                {
                    SQL.Append(String.Format(" {0} a.{1} IS NOT NULL AND CAST(a.{1} AS NVARCHAR(MAX)) <> ''",
                        Regex.IsMatch(SQL.ToString(), "WHERE") ? "AND" : "WHERE", column));
                }
            }

            if (!String.IsNullOrEmpty(extendSQL))
            {
                SQL.Append(String.Format(" {0} {1}", Regex.IsMatch(SQL.ToString(), "WHERE") ? "AND" : "WHERE", extendSQL));
            }

            #endregion

            //計算總比數
            int Count = unitOfWork.SqlQuery<int>(regexColumn.Replace(SQL.ToString(), "COUNT(*)"), Parameter.ToArray()).Single();

            List<SqlParameter> queryParameter = new List<SqlParameter>();
            foreach (var item in Parameter)
            {
                queryParameter.Add(new SqlParameter(item.ParameterName, item.Value));
            }

            #region 排序部分

            //產生排序與取得資料範圍用SQL
            string orderbySQL = String.Empty;
            string allCoumnSQL = getColumn;
            if (viewModel.Sort == "asc")
            {
                orderbySQL = " ORDER BY ALLDATA." + viewModel.SortData;
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY a.{0}) ROWNUM", viewModel.SortData);
            }
            else if (viewModel.Sort == "desc")
            {
                orderbySQL = " ORDER BY ALLDATA." + viewModel.SortData + " DESC";
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY a.{0} DESC) ROWNUM", viewModel.SortData);
            }
            else if (viewModel.Sort.Contains(","))
            {
                string[] sorts = viewModel.Sort.Split(new char[] { ',' });
                string[] sortDatas = viewModel.SortData.Split(new char[] { ',' });
                string orderby = String.Empty, rowOrderby = String.Empty;
                for (int i = 0; i < sorts.Length; i++)
                {
                    orderby += "ALLDATA." + sortDatas[i] + " " + sorts[i] + ",";
                    rowOrderby += "a." + sortDatas[i] + " " + sorts[i] + ",";
                }
                orderbySQL = String.Format(" ORDER BY {0}", orderby.Substring(0, orderby.Length - 1));
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY {0}) ROWNUM", rowOrderby.Substring(0, rowOrderby.Length - 1));
            }
            else
            {
                if (mainColumn != null && mainColumn.Count() > 0)
                    allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY a.{0}) ROWNUM", mainColumn[0]);
                else
                    allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY a.{0}) ROWNUM", selector.FirstOrDefault());
            }

            #endregion

            string querySQL = String.Format("SELECT * FROM ( {0} ) AS ALLDATA WHERE ALLDATA.ROWNUM BETWEEN @StartRow AND @EndRow {1}",
                regexColumn.Replace(SQL.ToString(), allCoumnSQL), orderbySQL);
            queryParameter.Add(new SqlParameter("@StartRow", viewModel.Start));
            queryParameter.Add(new SqlParameter("@EndRow", viewModel.Start + viewModel.Length));

            return Tuple.Create(Count, unitOfWork.SqlQuery<TEntity>(querySQL, queryParameter.ToArray()).ToList());
        }

        /// <summary>
        /// 範圍查詢
        /// </summary>
        /// <typeparam name="TEntity">查詢回傳型態</typeparam>
        /// <param name="key">範圍過濾條件欄位名稱</param>
        /// <param name="keytype">範圍過濾條件欄位型態</param>
        /// <param name="key2">其他過濾條件欄位名稱</param>
        /// <param name="keytype2">其他過濾條件欄位型態</param>
        /// <param name="arrFilter">其他過濾條件查詢值</param>
        /// <param name="arrStart">範圍過濾條件開頭查詢值</param>
        /// <param name="arrEnd">範圍過濾條件結尾查詢值</param>
        /// <param name="viewModel">查詢傳入ViewModel</param>
        /// <param name="extendSQL">附加查詢SQL</param>
        /// <param name="isFormatSortData">是否重新編排排序欄位(一般常用關聯)</param>
        /// <param name="tables">多表格查詢</param>
        /// <param name="tableExtends">多表格主要Enity</param>
        /// <param name="getColumn">查詢欄位</param>
        /// <returns>總筆數與資料查詢結果</returns>
        public static Tuple<int, List<TEntity>> GetMoreDetailSub<TEntity>(string[] key, string[] keytype,
            string[] key2, string[] keytype2, string[] arrFilter, string[] arrStart,
            string[] arrEnd, DetailRangeModel viewModel, String extendSQL = "", bool isFormatSortData = false,
            string[] tables = null, string[] tableExtends = null, string getColumn = "a.*", string tableName = null)
            where TEntity : class
        {
            IUnitOfWork unitOfWork = new UnitOfWork();

            //初始SQL查詢所需
            if (String.IsNullOrEmpty(tableName))
                tableName = typeof(TEntity).Name;

            #region 過濾條件

            StringBuilder FilterSQL = new StringBuilder();
            List<SqlParameter> Parameter = new List<SqlParameter>();

            #region 範圍過濾條件

            for (var i = 0; i <= arrStart.Length - 1; i++)
            {
                if (String.IsNullOrEmpty(arrStart[i]) && String.IsNullOrEmpty(arrEnd[i]))
                    continue;

                if (!String.IsNullOrEmpty(arrStart[i]) && !String.IsNullOrEmpty(arrEnd[i]))
                {
                    FilterSQL.Append(String.Format(" a.{0} BETWEEN CAST(@Start{0} AS {1}) AND CAST(@End{0} AS {1}) AND", key[i], GetSQLConvertTypeName(keytype[i])));
                    Parameter.Add(new SqlParameter("@Start" + key[i], arrStart[i]));
                    Parameter.Add(new SqlParameter("@End" + key[i], arrEnd[i]));
                }
                else if ((!String.IsNullOrEmpty(arrStart[i]) && String.IsNullOrEmpty(arrEnd[i])))
                {

                    FilterSQL.Append(String.Format(" a.{0} >= CAST(@Start{0} AS {1}) AND", key[i], GetSQLConvertTypeName(keytype[i])));
                    Parameter.Add(new SqlParameter("@Start" + key[i], arrStart[i]));
                }
                else if ((String.IsNullOrEmpty(arrStart[i]) && !String.IsNullOrEmpty(arrEnd[i])))
                {

                    FilterSQL.Append(String.Format(" a.{0} <= CAST(@End{0} AS {1}) AND", key[i], GetSQLConvertTypeName(keytype[i])));
                    Parameter.Add(new SqlParameter("@End" + key[i], arrEnd[i]));
                }
            }

            #endregion

            #region 其他過濾條件

            if (arrFilter.Length > 0)
            {
                for (var j = 0; j <= arrFilter.Length - 1; j++)
                {
                    if (String.IsNullOrEmpty(arrFilter[j]))
                    {
                        continue;
                    }
                    else if (!String.IsNullOrEmpty(arrFilter[j]))
                    {
                        switch (keytype2[j])
                        {
                            case "String":
                                FilterSQL.Append(String.Format(" a.{0} LIKE CAST('%'+ @Filter{1} +'%' AS NVARCHAR(MAX)) AND", key2[j], j));
                                break;
                            case "DateTime":
                                FilterSQL.Append(String.Format(" a.{0} = CAST(@Filter{1} AS DATETIME) AND", key2[j], j));
                                break;
                            default:
                                FilterSQL.Append(String.Format(" a.{0} = CAST(@Filter{1} AS {2}) AND", key2[j], j, GetSQLConvertTypeName(keytype2[j])));
                                break;
                        }

                        Parameter.Add(new SqlParameter("@Filter" + j, arrFilter[j]));
                    }
                }
            }

            #endregion

            //擴充SQL
            FilterSQL.Append(extendSQL);

            FilterSQL = ClearUselessSQL(FilterSQL);

            #endregion

            #region 多表格

            if (tables != null)
            {
                Regex regexFilter = new Regex(@"a\.");
                FilterSQL = new StringBuilder(regexFilter.Replace(FilterSQL.ToString(), ""));
                StringBuilder TableSQL = new StringBuilder("(");
                for (int i = 0; i < tables.Count(); i++)
                {
                    string table = tables[i];
                    string extand = tableExtends[i];
                    Type obj = Type.GetType("EIP_WebSystem.Models." + table + ",EIP_WebSystem.Models");
                    if (obj != null)
                    {
                        //查詢欄位 第一個為資料表名稱
                        string TableColumn = String.Format("'{0}' AS tablename", table);

                        //加入回傳型態的其他欄位
                        foreach (var property in typeof(TEntity).GetProperties())
                        {
                            if (property.PropertyType.Name.Contains("EntityCollection")
                                || property.PropertyType.Name.Contains("EntityReference")
                                || property.PropertyType.Name.Contains("EntityState")
                                || property.Name.Contains("EntityKey"))
                                continue;

                            if (obj.GetProperty(property.Name) != null)
                            {
                                TableColumn += String.Format(", {0}", property.Name);
                            }
                            else
                            {
                                TableColumn += String.Format(", NULL AS {0}", property.Name);
                            }
                        }

                        TableSQL.Append(String.Format(" SELECT {0} FROM {1} WHERE {2} UNION", TableColumn, table, FilterSQL.ToString()));
                        Regex regexTable = new Regex("%TABLE%"); //填入查詢表格用
                        TableSQL = new StringBuilder(regexTable.Replace(TableSQL.ToString(), table));
                        regexTable = new Regex("%TABLEEXTEND%");
                        TableSQL = new StringBuilder(regexTable.Replace(TableSQL.ToString(), extand));
                        regexTable = new Regex(@"(((WHERE)|(AND)){1}\s*UNION){1}", RegexOptions.IgnoreCase);
                        TableSQL = new StringBuilder(regexTable.Replace(TableSQL.ToString(), "UNION"));
                    }
                }
                ClearUselessSQL(TableSQL);
                TableSQL.Append(")");
                tableName = TableSQL.ToString(); //當作表格來查詢
                FilterSQL.Clear(); //過濾條件已填入多表格，所以清空
            }

            #endregion

            //產生SQL
            StringBuilder SQL = new StringBuilder(String.Format("SELECT %ALLCOLUMN% FROM {0} AS a WHERE {1}", tableName, FilterSQL.ToString()));
            SQL = ClearUselessSQL(SQL);

            Regex regexColumn = new Regex("%ALLCOLUMN%"); //取得欄位用

            //計算總比數
            int Count = unitOfWork.SqlQuery<int>(regexColumn.Replace(SQL.ToString(), "COUNT(*)"), Parameter.ToArray()).Single();

            List<SqlParameter> queryParameter = new List<SqlParameter>();
            foreach (var item in Parameter)
            {
                queryParameter.Add(new SqlParameter(item.ParameterName, item.Value));
            }

            #region 排序

            string orderbySQL = "";
            string allCoumnSQL = getColumn;
            if (viewModel.Sort == "asc")
            {
                orderbySQL = " ORDER BY ALLDATA." + viewModel.SortData;
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY a.{0}) ROWNUM", viewModel.SortData);
            }
            else if (viewModel.Sort == "desc")
            {
                orderbySQL = " ORDER BY ALLDATA." + viewModel.SortData + " DESC";
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY a.{0} DESC) ROWNUM", viewModel.SortData);
            }
            else if (viewModel.Sort == "customer_asc")
            {
                orderbySQL = " ORDER BY " + viewModel.SortData.Replace("a.", "ALLDATA.");
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY {0}) ROWNUM", viewModel.SortData);
            }
            else if (viewModel.Sort == "customer_desc")
            {
                orderbySQL = " ORDER BY " + viewModel.SortData.Replace("a.", "ALLDATA.") + " DESC";
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY {0} DESC) ROWNUM", viewModel.SortData);
            }
            else if (viewModel.Sort.Contains(","))
            {
                string[] sorts = viewModel.Sort.Split(new char[] { ',' });
                string[] sortDatas = viewModel.SortData.Split(new char[] { ',' });
                string orderby = String.Empty, rowOrderby = String.Empty;
                for (int i = 0; i < sorts.Length; i++)
                {
                    if (sorts[i].StartsWith("customer_"))
                    {
                        orderby += String.Format("{0} {1},", sortDatas[i].Replace("a.", "ALLDATA."), sorts[i].Replace("customer_", ""));
                        rowOrderby += String.Format("{0} {1},", sortDatas[i], sorts[i].Replace("customer_", ""));
                    }
                    else
                    {
                        orderby += String.Format("ALLDATA.{0} {1},", sortDatas[i], sorts[i]);
                        rowOrderby += String.Format("a.{0} {1},", sortDatas[i], sorts[i]);
                    }
                }
                orderbySQL = String.Format(" ORDER BY {0}", orderby.Substring(0, orderby.Length - 1));
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY {0}) ROWNUM", rowOrderby.Substring(0, rowOrderby.Length - 1));
            }
            else
            {
                allCoumnSQL += String.Format(", ROW_NUMBER() OVER (ORDER BY a.{0}) ROWNUM", key[key.Length - 1]);
            }

            #endregion

            #region 處理排序中的關聯欄位

            if (isFormatSortData)
            {
                string[] sortDatas = viewModel.SortData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var sortData in sortDatas)
                {
                    if ((sortData != GetFormatSortData(sortData)) && Regex.IsMatch(orderbySQL, sortData))
                    {

                        orderbySQL = orderbySQL.Replace(sortData, "OederBySortData");
                        allCoumnSQL += String.Format(", a.{0} AS OederBySortData", sortData);
                        allCoumnSQL = allCoumnSQL.Replace("a." + sortData, GetFormatSortData(sortData));

                        //從新產生SQL
                        SQL.Clear();
                        SQL.Append(String.Format("SELECT %ALLCOLUMN% FROM {0} {1} WHERE {2}",
                            tableName, GetReferenceSQLExtend(sortData), FilterSQL.ToString()));
                        SQL = ClearUselessSQL(SQL);
                    }
                }
            }

            #endregion

            //最後查詢SQL組合
            string querySQL = String.Format("SELECT * FROM ( {0} ) AS ALLDATA WHERE ALLDATA.ROWNUM BETWEEN @StartRow AND @EndRow {1}",
                regexColumn.Replace(SQL.ToString(), allCoumnSQL), orderbySQL);
            queryParameter.Add(new SqlParameter("@StartRow", viewModel.Start));
            queryParameter.Add(new SqlParameter("@EndRow", viewModel.Start + viewModel.Length));

            return Tuple.Create(Count, unitOfWork.SqlQuery<TEntity>(querySQL, queryParameter.ToArray()).ToList());
        }

        /// <summary>
        /// 取得C#型態所對應的SQL型態
        /// </summary>
        /// <param name="type">型態名稱</param>
        /// <returns>對應SQL型態</returns>
        private static string GetSQLConvertTypeName(string type)
        {
            switch (type.ToLower()) //全部轉小寫比較
            {
                case "string":
                    return "NVARCHAR(MAX)";
                case "timespan":
                    return "TIME";
                case "date":
                    return "DATE";
                case "datetime":
                    return "DATETIME";
                case "int":
                    return "INT";
                case "byte":
                    return "TINYINT";
                case "boolean":
                case "bool":
                    return "BIT";
                default:
                    return type;
            }
        }

        /// <summary>
        /// 清除結尾不必要的SQL(目前判斷AND、WHERE、UNION)
        /// </summary>
        /// <param name="sql">StringBuilder型態的SQL字串</param>
        private static StringBuilder ClearUselessSQL(StringBuilder sql)
        {
            if (sql.Length > 0)
            {
                Regex regex = new Regex(@"((WHERE)|(AND)){1}\s*$", RegexOptions.IgnoreCase);
                return new StringBuilder(regex.Replace(sql.ToString(), ""));
            }
            return sql;
        }

        /// <summary>
        /// 取得關聯排序欄位
        /// </summary>
        /// <param name="sortData">欄位名稱</param>
        /// <returns>對應關聯欄位名稱</returns>
        private static string GetFormatSortData(string sortData)
        {
            switch (sortData.ToLower()) //全部轉小寫比較
            {
                case "sname":
                    return "SL12.Name";
                case "cname":
                    return "SL11.Name";
                case "pname":
                    return "SL00.Name";
                case "pname2":
                    return "SL00.Name";
                case "pname6":
                    return "SL00.Name";
                case "pjname":
                    return "SL559.Pjname";
                default:
                    return sortData;
            }
        }

        /// <summary>
        /// 取得關聯排序欄位的關聯擴充SQL
        /// </summary>
        /// <param name="sortData">欄位名稱</param>
        /// <returns>對應關聯欄位擴充SQL</returns>
        private static string GetReferenceSQLExtend(string sortData)
        {
            switch (sortData.ToLower()) //全部轉小寫比較
            {
                case "sname":
                    return " AS a LEFT JOIN SL12 ON a.sid = SL12.sid";
                case "cname":
                    return " AS a LEFT JOIN SL11 ON a.cid = SL11.cid";
                case "pname":
                    return " AS a LEFT JOIN SL00 ON a.pid = SL00.pid";
                case "pname2":
                    return " AS a LEFT JOIN SL00 ON a.pid2 = SL00.pid";
                case "pname6":
                    return " AS a LEFT JOIN SL00 ON a.pid6 = SL00.pid";
                case "pjname":
                    return " AS a LEFT JOIN SL559 ON a.pjno = SL559.pjno";
                default:
                    return " AS a";
            }
        }
    }
}