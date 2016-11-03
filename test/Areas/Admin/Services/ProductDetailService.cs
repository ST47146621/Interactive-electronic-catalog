using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.Models;
using test.Services.Generic;

namespace test.Areas.Admin.Services
{
    public class ProductDetailService : DetailViewService
    {
        public ProductDetailModel GetViewData()
        {
            ProductDetailModel model = new ProductDetailModel(true);
            model.Select1 = 0;
            model.Select2 = 1;
            model.Select3 = 1;
            return model;
        }

        public DetailSearchDataModel<ProductDetailGridModel> Search(ProductDetailModel jsonData)
        {
            string[] Selector = { "productid", "productname" };

            var SearchResult = DetailService.GetDetailSub<ProductDetailGridModel>
                (Selector, jsonData, new string[] { "productid" }, tableName: "Product");

            int page = SearchResult.Item1;

            System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext);
            List<ProductDetailGridModel> aaData = SearchResult.Item2;
            foreach (ProductDetailGridModel item in aaData)
            {
                item.productimgurl1 = "<div class='glyphicon glyphicon-menu-right' data-id='" + item.productid + "' data-img='" + HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + item.productimgurl1 + "'></div>";
            }

            return new DetailSearchDataModel<ProductDetailGridModel> { draw = jsonData.Draw, recordsTotal = page, recordsFiltered = page, data = aaData };
        }
    }
}